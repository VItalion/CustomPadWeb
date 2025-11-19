namespace CustomPadWeb.AuthService.Domain
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
