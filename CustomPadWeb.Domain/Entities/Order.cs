using CustomPadWeb.Domain.Abstractions;

namespace CustomPadWeb.Domain.Entities
{
    public enum OrderStatus { Draft, Pending, Processing, Completed, Cancelled }


    public class Order : Entity, IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Draft;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        private readonly List<CustomizationOption> _options = new();
        public IReadOnlyCollection<CustomizationOption> Options => _options.AsReadOnly();


        protected Order() { }


        public Order(Guid userId)
        {
            UserId = userId;
        }


        public void AddOption(CustomizationOption option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));
            _options.Add(option);
        }


        public void SetStatus(OrderStatus status) => Status = status;
    }
}
