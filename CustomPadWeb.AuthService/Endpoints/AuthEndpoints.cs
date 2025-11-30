using CustomPadWeb.AuthService.Exceptions;
using CustomPadWeb.AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace CustomPadWeb.AuthService.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/register", async (RegisterRequest req, IAuthService auth) =>
            {
                try
                {
                    await auth.RegisterAsync(req.Email, req.Password);
                    return Results.Ok();
                }
                catch (AlredyExistsException ex)
                {
                    return Results.Conflict(ex.Message);
                }
            });

            app.MapPost("/auth/login", async ([FromBody] LoginRequest req, IAuthService auth) =>
            {
                try
                {
                    var result = await auth.LoginAsync(req.Email, req.Password);
                    return Results.Ok(result);
                }
                catch (InvalidCredentialException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            app.MapPost("/auth/refresh", async (RefreshRequest req, IAuthService auth) =>
            {
                try
                {
                    var result = await auth.RefreshAsync(req.RefreshToken);
                    return Results.Ok(result);
                }
                catch (InvalidTokenException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });
        }

        public record RegisterRequest(string Email, string Password);
        public record LoginRequest(string Email, string Password);
        public record RefreshRequest(string RefreshToken);
    }
}
