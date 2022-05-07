using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using SpaceAdventures.API.Configurations;
using SpaceAdventures.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(config));


// Add services to the container.
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));


// Injection DB Service
builder.Services.AddInfrastructure(configuration);

// Injection Application Services
builder.Services.AddApplication();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Api Versioned Explorer Configuration  -- Swagger Config
builder.Services.AddVersionedApiExplorerConfig();

builder.Services.AddSwaggerGen();

// Swagger Description Configuration
builder.Services.ConfigureOptions<SwaggerConfig>();

// Api Versioning Configuration
builder.Services.AddApiVersioningConfig();


builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
    options.ReturnHttpNotAcceptable = true;
}).AddFluentValidation();

var app = builder.Build();

// Our Custom Exception Middleware
app.UseExceptionMiddleware();

//  Our Log Request Middleware
// app.UseLogRequestMiddleware();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
        }
    });
}





app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




