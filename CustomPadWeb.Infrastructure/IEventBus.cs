namespace CustomPadWeb.Infrastructure
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event, CancellationToken token = default);
    }
}
