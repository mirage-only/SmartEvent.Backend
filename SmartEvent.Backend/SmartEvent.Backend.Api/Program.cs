using SmartEvent.Backend.Api.Handlers;
using SmartEvent.Backend.Application;
using SmartEvent.Backend.Infrastructure;
using SmartEvent.Backend.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure(configuration);

services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
    .AddPolicy("AtLeastTeacher", policy => policy.RequireRole("Teacher", "Admin"))
    .AddPolicy("AtLeastStudent", policy => policy.RequireRole("Student", "Teacher", "Admin"));

services.AddControllers();

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();

services.AddProblemDetails();
services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();