using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CustomPadWeb.AuthService.IntegrationEvents
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;

        public RabbitMqEventBus(IConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishAsync<T>(T @event)
        {
            using var channel = await _connection.CreateChannelAsync().ConfigureAwait(false);
            await channel.ExchangeDeclareAsync("auth_exchange", ExchangeType.Fanout, durable: true).ConfigureAwait(false);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));

            await channel.BasicPublishAsync(
                exchange: "auth_exchange",
                routingKey: "",
                mandatory: false,
                basicProperties: new BasicProperties(),
                body: body)
                .ConfigureAwait(false);
        }
    }
}
