using Blazored.LocalStorage;
using CustomPadWeb.Frontend.Client.Clients;
using CustomPadWeb.Frontend.Client.Providers;
using CustomPadWeb.Frontend.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace CustomPadWeb.Frontend.Client
{
    public class Common
    {
        public static void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            services.AddScoped<IAuthService, JwtAuthenticationStateProvider>();
        }

        public static void ConfigureApiClients(IServiceCollection services, string baseAddress, string authAddress)
        {
            services.AddHttpClient<OrderApiClient>(client =>
            {
                // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
                // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
                client.BaseAddress = new(baseAddress);
            });

            services.AddHttpClient<AuthApiClient>(client =>
            {
                // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
                // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
                client.BaseAddress = new(authAddress);
            });
        }
    }
}
