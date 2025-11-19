using CustomPadWeb.AuthService.Data;
using CustomPadWeb.AuthService.Domain;
using CustomPadWeb.AuthService.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace CustomPadWeb.AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _db;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtService _jwt;
        private readonly IEventBus _eventBus;

        public AuthService(
            AuthDbContext db,
            IPasswordHasher hasher,
            IJwtService jwt,
            IEventBus eventBus)
        {
            _db = db;
            _hasher = hasher;
            _jwt = jwt;
            _eventBus = eventBus;
        }

        public async Task<User> RegisterAsync(string email, string password)
        {
            if (await _db.Users.AnyAsync(x => x.Email == email))
                throw new Exception("User already exists.");

            var user = new User
            {
                Email = email,
                PasswordHash = _hasher.Hash(password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            await _eventBus.PublishAsync(
                new UserRegisteredIntegrationEvent(user.Id, user.Email));

            return user;
        }

        public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
        {
            var user = await _db.Users.Include(u => u.RefreshTokens)
                                      .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || !_hasher.Verify(user.PasswordHash, password))
                throw new Exception("Invalid credentials.");

            var access = _jwt.GenerateAccessToken(user);
            var refresh = _jwt.GenerateRefreshToken();

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refresh,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _db.SaveChangesAsync();

            return (access, refresh);
        }

        public async Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken)
        {
            var token = await _db.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == refreshToken);

            if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Invalid refresh token.");

            token.IsRevoked = true;

            var newRefreshToken = _jwt.GenerateRefreshToken();
            token.User.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            var access = _jwt.GenerateAccessToken(token.User);

            await _db.SaveChangesAsync();

            return (access, newRefreshToken);
        }
    }
}
