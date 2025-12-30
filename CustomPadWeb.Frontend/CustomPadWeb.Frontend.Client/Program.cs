using Blazored.LocalStorage;
using CustomPadWeb.Frontend.Client.Clients;
using CustomPadWeb.Frontend.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();


await builder.Build().RunAsync();
