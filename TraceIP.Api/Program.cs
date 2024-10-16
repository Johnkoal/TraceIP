using TraceIP.Application.Services;
using TraceIP.Data.Repositories;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;
using TraceIP.Infraestructure.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Settings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<ExternalServices>(builder.Configuration.GetSection("ExternalServices"));

builder.Services.AddScoped<ILoggerService, LoggerService>();

// Repositories
builder.Services.AddScoped<IFixerRepository, FixerRepository>();
builder.Services.AddScoped<IGeoPluginRepository, GeoPluginRepository>();
builder.Services.AddScoped<IIpApiRepository, IpApiRepository>();
builder.Services.AddScoped<ITimezonedbRepository, TimezonedbRepository>();

// Services application
builder.Services.AddScoped<ITraceIpService, TraceIpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
