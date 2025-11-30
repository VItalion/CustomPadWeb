using CustomPadWeb.AuthService.Data;
using CustomPadWeb.AuthService.IntegrationEvents;
using CustomPadWeb.AuthService.Services;
using RabbitMQ.Client;
using Microsoft.EntityFrameworkCore;
using CustomPadWeb.AuthService.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration["Postgres:AuthDb"]));

// Add RabbitMQ Connection
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory
    {
        HostName = builder.Configuration["RabbitMQ:Host"] ?? "localhost",
        Port = int.TryParse(builder.Configuration["RabbitMQ:Port"], out var portVal) ? portVal : 5672,
        UserName = builder.Configuration["RabbitMQ:User"] ?? "guest",
        Password = builder.Configuration["RabbitMQ:Password"] ?? "guest",
    };

    return factory.CreateConnectionAsync().Result;
});

builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

var app = builder.Build();
app.MapAuthEndpoints();
app.Run();
