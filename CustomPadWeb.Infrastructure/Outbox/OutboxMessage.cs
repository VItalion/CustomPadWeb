using System.Text.Json;

namespace CustomPadWeb.Infrastructure.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime OccurredOnUtc { get; private set; } = DateTime.UtcNow;
        public string Type { get; private set; }
        public string Content { get; private set; }
        public DateTime? ProcessedOnUtc { get; private set; }

        private OutboxMessage() { } // EF Core

        public OutboxMessage(object integrationEvent)
        {
            Type = integrationEvent.GetType().FullName ?? "UnknownEvent";
            Content = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType());
        }

        public void MarkProcessed()
        {
            ProcessedOnUtc = DateTime.UtcNow;
        }
    }
}
