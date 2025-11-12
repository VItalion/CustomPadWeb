namespace CustomPadWeb.Infrastructure
{
    public record OrderSubmittedIntegrationEvent(Guid OrderId, Guid UserId, DateTime SubmittedAt);
}
