using CustomPadWeb.AuthService.Domain;

namespace CustomPadWeb.AuthService.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string password);
        Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password);
        Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken);
    }
}
