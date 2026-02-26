using SmartEvent.Backend.Application;
using SmartEvent.Backend.Infrastructure;
using SmartEvent.Backend.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure(configuration);

services.AddControllers();

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();

var app = builder.Build();

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