using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Identity.Application.Exceptions;

internal sealed class EmailAlreadyUsedException(string email)
    : BaseException($"Email '{email}' is already in use", "Identity.EmailInUse");

internal sealed class UsernameAlreadyUsedException(string username)
    : BaseException($"Username '{username}' is already in use", "Identity.UsernameInUse");

internal sealed class InvalidEmailProviderException(string emailProvider)
    : BaseException($"'{emailProvider}' is not valid email provider.", "Identity.InvalidEmailProvider");

internal sealed class InvalidEmailConfirmationToken()
    : BaseException("Email confirmation token is invalid", "Identity.InvalidEmailConfirmationToken");

internal sealed class InvalidCredentialsException()
    : UnauthorizedException("Invalid credentials.", "Identity.InvalidCredentials");