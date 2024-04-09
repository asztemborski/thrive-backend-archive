using MediatR;

namespace Thrive.Modules.Identity.Shared.Events;

public sealed record IdentityUserCreatedEvent(string Email, string Username) : INotification;