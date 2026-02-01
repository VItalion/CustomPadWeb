using System.Net.Http.Json;

namespace CustomPadWeb.Frontend.Client.Clients
{
    public class AuthApiClient
    {
        private readonly HttpClient _http;

        public AuthApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            var response = await _http.PostAsJsonAsync("/auth/login", request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<LoginResult>().ConfigureAwait(false);
            else
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception($"Login failed: {content}");
            }
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var response = await _http.PostAsJsonAsync("/auth/register", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<RefreshResult> RefreshTokenAsync(RefreshRequest request)
        {
            var response = await _http.PostAsJsonAsync("/auth/refresh", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RefreshResult>().ConfigureAwait(false);
        }
    }

    public record LoginRequest(string Email, string Password);
    public record LoginResult(string accessToken, string refreshToken);
    public record RegisterRequest(string Email, string Password);
    public record RefreshRequest(string RefreshToken);
    public record RefreshResult(string AccessToken, string RefreshToken);
}
