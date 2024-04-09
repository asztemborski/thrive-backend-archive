using Fleet.Shared.Abstractions.Exceptions;

namespace Fleet.Modules.Identity.Application.Exceptions;

internal sealed class EmailAlreadyUsedException(string email) 
: BadRequestException($"Email {email} is already in use.");

internal class UsernameAlreadyUsedException(string username)
    : BadRequestException($"Username {username} is already in use");

internal sealed class InvalidCredentialsException() : BadRequestException("Invalid credentials.");

public sealed class UnauthorizedException()
    : Fleet.Shared.Abstractions.Exceptions.UnauthorizedException("Unauthorized.");

internal sealed class InvalidEmailProviderException(string emailProvider)
    : BadRequestException($"{emailProvider} is not valid email provider.");

internal sealed class InvalidEmailConfirmationToken() : BadRequestException("Email confirmation token is invalid");