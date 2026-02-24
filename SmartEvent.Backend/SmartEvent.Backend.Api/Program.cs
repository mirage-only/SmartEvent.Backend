using SmartEvent.Backend.Application;
using SmartEvent.Backend.Infrastructure;
using SmartEvent.Backend.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();