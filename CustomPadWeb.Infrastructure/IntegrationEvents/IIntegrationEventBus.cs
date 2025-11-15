namespace CustomPadWeb.Infrastructure.IntegrationEvents
{
    public interface IIntegrationEventBus
    {
        Task PublishAsync(IntegrationEvent @event, CancellationToken token = default);
    }
}
