using CustomPadWeb.AuthService.Domain;

namespace CustomPadWeb.AuthService
{
    public static class Extentions
    {
        public static void AddToRole(this User user, Role role)
        {
            user.Role = role;
            role.Users ??= [];
            role.Users.Add(user);
        }

        public static void AddRefreshToken(this User user, RefreshToken refreshToken)
        {
            refreshToken.User = user;
            user.RefreshTokens ??= [];
            user.RefreshTokens.Add(refreshToken);
        }
    }
}
