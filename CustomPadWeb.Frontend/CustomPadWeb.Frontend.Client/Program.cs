using Blazored.LocalStorage;
using CustomPadWeb.Frontend.Client;
using CustomPadWeb.Frontend.Client.Providers;
using CustomPadWeb.Frontend.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
Common.ConfigureApiClients(builder.Services, baseAddress: "https://localhost:7433", authAddress: "https://localhost:7032");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, JwtAuthenticationStateProvider>();

await builder.Build().RunAsync();
