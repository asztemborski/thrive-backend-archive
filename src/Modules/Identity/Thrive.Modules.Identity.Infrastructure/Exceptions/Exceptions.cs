namespace Thrive.Modules.Identity.Infrastructure.Exceptions;

internal sealed class UnauthorizedException() 
    : Thrive.Shared.Abstractions.Exceptions.UnauthorizedException("Unauthorized.", "Identity.Unauthorized");
    