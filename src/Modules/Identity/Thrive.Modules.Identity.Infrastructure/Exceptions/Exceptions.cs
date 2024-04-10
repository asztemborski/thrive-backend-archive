using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Identity.Infrastructure.Exceptions;

internal static class InfrastructureExceptions
{
    public static BaseException UnauthorizedException() => new UnauthorizedException("Unauthorized.");
}