using RabbitMQ.Client;
using System.Text.Json;

namespace CustomPadWeb.Infrastructure.IntegrationEvents
{
    public class RabbitMqIntegrationEventBus : IIntegrationEventBus
    {
        private readonly IConnection _connection;

        public RabbitMqIntegrationEventBus(IConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishAsync(IntegrationEvent @event, CancellationToken token = default)
        {
            await using var channel = await _connection.CreateChannelAsync();

            const string exchangeName = "custompad_integration_exchange";
            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout, durable: true, cancellationToken: token);

            var eventName = @event.GetType().Name;
            var body = new ReadOnlyMemory<byte>(JsonSerializer.SerializeToUtf8Bytes(@event));

            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: eventName,
                mandatory: false,
                basicProperties: new BasicProperties(),
                body: body,
                cancellationToken: token);
        }
    }
}
