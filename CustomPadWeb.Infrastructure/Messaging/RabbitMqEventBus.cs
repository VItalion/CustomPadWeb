using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text.Json;

namespace CustomPadWeb.Infrastructure.Messaging
{
    public class RabbitMqEventBus : IEventBus, IAsyncDisposable
    {
        private readonly IConnection _connection;
        private readonly ILogger<RabbitMqEventBus> _logger;
        private readonly string _exchangeName = "custompad_exchange";

        private bool _inited = false;

        public RabbitMqEventBus(IConnection connection,
                                ILogger<RabbitMqEventBus> logger)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            await using var channel = await _connection.CreateChannelAsync();
            if (!_inited)
            {
                // Declare the exchange once (durable topic)
                await channel.ExchangeDeclareAsync(exchange: _exchangeName,
                                        type: ExchangeType.Topic,
                                        durable: true);
                _logger.LogInformation("Exchange '{ExchangeName}' declared.", _exchangeName);
            }

            var eventName = typeof(T).Name;
            var body = JsonSerializer.SerializeToUtf8Bytes(@event);

            BasicProperties props = new()
            {
                Persistent = true   // make message persistent if required
            };

            await channel.BasicPublishAsync(
                exchange: _exchangeName,
                routingKey: eventName,
                mandatory: false,
                basicProperties: props,
                body: body);

            _logger.LogInformation("Published event '{EventName}' via RabbitMQ.", eventName);
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_connection.IsOpen)
                {
                    await _connection.CloseAsync();
                    await _connection.DisposeAsync();
                    _logger.LogInformation("RabbitMQ connection closed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing RabbitMQ connection.");
            }
        }
    }
}
