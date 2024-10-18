using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Reflection;
using TraceIP.Application.Interfaces;
using TraceIP.Application.Services;
using TraceIP.Data.Context;
using TraceIP.Data.Repositories;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;
using TraceIP.Infraestructure.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TraceIP.Api",
        Description = "API que proporciona información de IP's"
    });
    x.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault()?.DisplayName ?? x.FullName);
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add DbContext
builder.Services.AddDbContext<DataContext>(opts => opts.UseSqlite("Data Source=Database.db"));

// Settings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<ExternalServices>(builder.Configuration.GetSection("ExternalServices"));

builder.Services.AddScoped<ILoggerService, LoggerService>();

// Repositories
builder.Services.AddScoped<IFixerRepository, FixerRepository>();
builder.Services.AddScoped<IGeoPluginRepository, GeoPluginRepository>();
builder.Services.AddScoped<IIpApiRepository, IpApiRepository>();
builder.Services.AddScoped<ITimezonedbRepository, TimezonedbRepository>();
builder.Services.AddScoped<IIpResultRepository, IpResultRepository>();

// Services application
builder.Services.AddScoped<ITraceIpService, TraceIpService>();
builder.Services.AddScoped<IHaversineService, HaversineService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
