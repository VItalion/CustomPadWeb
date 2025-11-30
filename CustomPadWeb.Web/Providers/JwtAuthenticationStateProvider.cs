using Blazored.LocalStorage;
using CustomPadWeb.Web.Clients;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CustomPadWeb.Web.Providers
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string AccessTokenKey = "accessToken";
        private const string RefreshTokenKey = "refreshToken";

        private readonly AuthApiClient _authApi;
        private readonly ILocalStorageService _storageService;

        public JwtAuthenticationStateProvider(ILocalStorageService storageService, AuthApiClient authApi)
        {
            _storageService = storageService;
            _authApi = authApi;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token;

            try
            {
                token = await _storageService.GetItemAsync<string>(AccessTokenKey).ConfigureAwait(false) ?? string.Empty;
            }
            catch (InvalidOperationException ex)
            {
                token = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async Task SignInAsync(string accessToken, string refreshToken)
        {
            await _storageService.SetItemAsync(AccessTokenKey, accessToken).ConfigureAwait(false);
            await _storageService.SetItemAsync(RefreshTokenKey, refreshToken).ConfigureAwait(false);

            var authState = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task SignOutAsync()
        {
            await _storageService.RemoveItemAsync(AccessTokenKey).ConfigureAwait(false);
            await _storageService.RemoveItemAsync(RefreshTokenKey).ConfigureAwait(false);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        // Optionally: method to refresh the token
        public async Task<string?> RefreshTokenAsync()
        {
            var refresh = await _storageService.GetItemAsync<string>(RefreshTokenKey).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(refresh))
                return null;

            var result = await _authApi.RefreshTokenAsync(new RefreshRequest(refresh));
            await SignInAsync(result.AccessToken, result.RefreshToken);
            return result.AccessToken;
        }
    }
}
