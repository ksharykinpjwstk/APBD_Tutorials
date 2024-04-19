using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tutorial7.Showcase.Application;
using Tutorial7.Showcase.Infrastrucutre;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Docker")!;

builder.Services.AddSingleton<ICountryRepository>(countryRepository => new CountryRepository(connectionString));
builder.Services.AddSingleton<ICityRepository>(cityRepository => new CityRepository(connectionString));
builder.Services.AddSingleton<ISchoolRepository>(schoolRepository => new SchoolRepository(connectionString));

builder.Services.AddScoped<ISchoolService, SchoolService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("schools", async (ISchoolService schoolService) => {
    return await schoolService.GetAll();
})
.WithName("Get schools")
.WithOpenApi();

app.MapPost("schools", async (ISchoolService schoolService, AddSchoolDTO schoolDTO) => {
    try 
    {
        var result = await schoolService.Add(schoolDTO);
        return result ? Results.Created() : Results.BadRequest();
    }
    catch (ArgumentException ae) 
    {
        return Results.BadRequest(ae.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(title: "Something went wrong. Try later.", statusCode: 500);
    } 
});

app.Run();