using CustomPadWeb.Application.Services.Contracts;
using CustomPadWeb.Common.ViewModels;
using CustomPadWeb.Domain.DomainEvents;
using CustomPadWeb.Domain.Entities;
using CustomPadWeb.Infrastructure;

namespace CustomPadWeb.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public OrderService(
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<Guid> CreateOrderAsync(Guid userId, CreateOrderViewModel model, CancellationToken token = default)
        {
            // Map VM → Domain Entity
            var order = new Order(userId)
            {
                CreatedAt = DateTime.UtcNow,
                CustomerEmail = model.CustomerEmail,
                Notes = model.Notes,
                Configuration = new GamepadConfiguration
                {
                    Name = $"Custom config for {model.CustomerEmail}",
                }
            };

            // Add entity
            await _unitOfWork.Orders.AddAsync(order, token);

            // Save changes
            await _unitOfWork.SaveChangesAsync(token);

            return order.Id;
        }
    }
}