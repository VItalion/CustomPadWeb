namespace CustomPadWeb.AuthService.IntegrationEvents
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event);
    }
}
