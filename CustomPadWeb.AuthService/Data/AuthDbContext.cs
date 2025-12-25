using CustomPadWeb.AuthService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomPadWeb.AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        private readonly Guid AdminRoleId = Guid.Parse("943ad13e-4aa5-46a5-82e4-c3c15f122f53");
        private readonly Guid UserRoleId = Guid.Parse("96eb6646-6538-4b0a-b855-e34a4ee9fe3c");

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasMany<User>()
                    .WithOne(u => u.Role!)
                    .HasForeignKey(u => u.Id)
                    .IsRequired();
                b.HasData(
                    new Role { Id = UserRoleId, Name = Constants.UserRoleName },
                    new Role { Id = AdminRoleId, Name = Constants.AdminRoleName }
                );
            });

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.Email).IsRequired();
                b.Property(u => u.PasswordHash).IsRequired();
                b.HasOne<Role>()
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .IsRequired();
            });
        }
    }
}
