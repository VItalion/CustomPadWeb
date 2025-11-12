using CustomPadWeb.Domain.Abstractions;
using CustomPadWeb.Domain.Configurations;
using CustomPadWeb.Domain.DomainEvents;
using CustomPadWeb.Domain.Entities;
using CustomPadWeb.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CustomPadWeb.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public DbSet<Suggestion> Suggestions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<CustomizationOption> CustomizationOptions { get; set; } = null!;


        public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher) : base(options) 
        {
            _dispatcher = dispatcher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SuggestionConfiguration());


            // Configure value object mapping for Email (store as string)
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Email)
                    .HasConversion(
                        email => email.Value,          // to string for DB
                        value => new Email(value))     // from string to VO
                    .IsRequired()
                    .HasMaxLength(320);
            });


            // Additional configurations for Order / CustomizationOption
            modelBuilder.Entity<Order>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.Status).IsRequired();
                b.HasMany(o => o.Options)
                    .WithOne(o => o.Order)
                    .HasForeignKey(o => o.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entity in entities)
            {
                var events = entity.DomainEvents.ToList();
                entity.ClearDomainEvents();

                foreach (var domainEvent in events)
                    await _dispatcher.DispatchAsync(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
