namespace CustomPadWeb.AuthService.IntegrationEvents
{
    public record UserRegisteredIntegrationEvent(Guid UserId, string Email);
}
