using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Tutorial12.API.Helpers;
using Tutorial12.API.Services;

// Creating a builder with parameters from program arguments
var builder = WebApplication.CreateBuilder(args);
// Copy config data from builder's field
var config = builder.Configuration;

// Adding Entity Framework service, using our context and providing connection string from appsettings.json
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Docker")));
// Add services, that responsible for generating tokens
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
// Add token-based authentication
// First step - specify authentication options for ASP.NET Core
// Second step - settings up Jwt Bearer token
builder.Services.AddAuthentication(a =>
    {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            // Who generates token. Specifying address of host
            ValidIssuer = config["JwtSettings:Issuer"],
            // Which web application will use it.
            ValidAudience = config["JwtSettings:Audience"],
            // Provide random string key as a byte array
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
            // Require validation, who generated token
            ValidateIssuer = true,
            // Require validation, who can use this token
            ValidateAudience = true,
            // Require check token expiration
            ValidateLifetime = true,
            // Require check key signature
            ValidateIssuerSigningKey = true
        };
    });

// Tell ASP.NET Core that we will have authorization
builder.Services.AddAuthorization();
// Tell ASP.NET Core that we use controller - based approach
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Add swagger support
builder.Services.AddSwaggerGen(options =>
{
    // We tell swagger that we have authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    // We tell swagger that for most endpoints we need authentication
    // Thanks to that, you can use token on swagger page
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Option 1 of global handling provided by Microsoft that uses specification
// It will intercept exceptions and return 500 with standard body message, not as a stacktrace.
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//Option 1 of global handling: Part 2
app.UseExceptionHandler();

// Option 2: Custom middleware global error handler
//app.UseMiddleware<ExceptionHandlingMiddleware>();

// Needs for enabling authentication
app.UseAuthentication();
// Needs for enabling authorization
app.UseAuthorization();
// Needs for controller-based approach
app.MapControllers();

app.Run();