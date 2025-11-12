using RabbitMQ.Client;
using System.Text.Json;

namespace CustomPadWeb.Infrastructure
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;
        public RabbitMqEventBus(IConnection connection) => _connection = connection;

        public async Task PublishAsync<T>(T @event, CancellationToken token = default)
        {
            using var channel = _connection.CreateModel();
            var body = JsonSerializer.SerializeToUtf8Bytes(@event);
            var eventName = typeof(T).Name;

            channel.BasicPublish(exchange: "custompad_exchange",
                                 routingKey: eventName,
                                 basicProperties: null,
                                 body: body);
            await Task.CompletedTask;
        }
    }
}
