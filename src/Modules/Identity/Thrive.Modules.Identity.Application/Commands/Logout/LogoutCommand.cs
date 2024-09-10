using MediatR;

namespace Thrive.Modules.Identity.Application.Commands.Logout;

public sealed class LogoutCommand : IRequest
{
    public string RefreshToken { get; init; } = string.Empty;
}