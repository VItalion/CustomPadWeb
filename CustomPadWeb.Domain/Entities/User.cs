using CustomPadWeb.Domain.Abstractions;
using CustomPadWeb.Domain.ValueObjects;

namespace CustomPadWeb.Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; } = string.Empty; // store hashed password
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        protected User() { }


        public User(Email email, string passwordHash)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }


        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
        }
    }
}
