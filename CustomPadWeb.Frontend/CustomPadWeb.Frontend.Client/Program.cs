using Blazored.LocalStorage;
using CustomPadWeb.Frontend.Client;
using CustomPadWeb.Frontend.Client.Providers;
using CustomPadWeb.Frontend.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();

Common.ConfigureApiClients(builder.Services, "https://localhost:7433", "https://localhost:7032");
//Common.ConfigureCommonServices(builder.Services);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, JwtAuthenticationStateProvider>();

await builder.Build().RunAsync();
