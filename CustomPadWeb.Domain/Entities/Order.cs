using CustomPadWeb.Domain.Abstractions;
using CustomPadWeb.Domain.DomainEvents;

namespace CustomPadWeb.Domain.Entities
{
    public enum OrderStatus { Draft, Pending, Processing, Completed, Cancelled }


    public class Order : Entity, IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public Guid ConfigurationId { get; set; }
        public virtual GamepadConfiguration Configuration { get; set; }

        private readonly List<CustomizationOption> _options = new();
        public IReadOnlyCollection<CustomizationOption> Options => _options.AsReadOnly();

        protected Order() { }

        public Order(Guid userId)
        {
            UserId = userId;
            AddDomainEvent(new OrderCreatedDomainEvent(Id));
        }

        public void AddOption(CustomizationOption option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));
            _options.Add(option);
        }

        public void SetStatus(OrderStatus status) => Status = status;
    }
}
