using CustomPadWeb.AuthService.Services;

namespace CustomPadWeb.AuthService.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/register", async (RegisterRequest req, IAuthService auth) =>
            {
                await auth.RegisterAsync(req.Email, req.Password);
                return Results.Ok();
            });

            app.MapPost("/auth/login", async (LoginRequest req, IAuthService auth) =>
            {
                var result = await auth.LoginAsync(req.Email, req.Password);
                return Results.Ok(result);
            });

            app.MapPost("/auth/refresh", async (RefreshRequest req, IAuthService auth) =>
            {
                var result = await auth.RefreshAsync(req.RefreshToken);
                return Results.Ok(result);
            });
        }

        public record RegisterRequest(string Email, string Password);
        public record LoginRequest(string Email, string Password);
        public record RefreshRequest(string RefreshToken);
    }
}
