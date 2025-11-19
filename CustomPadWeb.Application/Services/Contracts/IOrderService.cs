using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.Application.Services.Contracts
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid userId, CreateOrderViewModel request, CancellationToken cancellationToken = default);
    }
}
