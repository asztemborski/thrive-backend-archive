using MediatR;

namespace Fleet.Modules.Identity.Shared.Events;

public sealed record IdentityUserCreatedEvent(string Email, string Username) : INotification;