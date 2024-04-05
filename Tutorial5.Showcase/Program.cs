var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/students", async (IStudentRepository studentRepository) =>
    await studentRepository.GetAll())
    .WithOpenApi();

app.MapGet("/students/{id}", (IStudentRepository studentRepository, int id) => studentRepository.Get(id))
.WithOpenApi();

app.Run();
