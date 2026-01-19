namespace CustomPadWeb.Frontend.Client.Services
{
    public interface IAuthService
    {
        Task SignInAsync(string accessToken, string refreshToken);
        Task SignOutAsync();
        Task<string?> RefreshTokenAsync();
    }
}
