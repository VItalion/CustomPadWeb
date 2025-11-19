using CustomPadWeb.AuthService.Domain;
using System.Security.Claims;

namespace CustomPadWeb.AuthService.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateAccessToken(string token);
    }
}
