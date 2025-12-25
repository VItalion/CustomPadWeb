using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CustomPadWeb.AuthService.IntegrationEvents
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqEventBus> _logger;

        public RabbitMqEventBus(IConnectionFactory connectionFactory, ILogger<RabbitMqEventBus> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T @event)
        {
            try
            {
                using var connection = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false);
                using var channel = await connection.CreateChannelAsync().ConfigureAwait(false);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish event to RabbitMQ");
                throw;
            }
        }
    }
}
