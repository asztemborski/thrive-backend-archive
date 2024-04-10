using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Identity.Application.Exceptions;

internal static class ApplicationExceptions
{
    public static BaseException EmailAlreadyUsedException(string email)
         => new BadRequestException($"Email '{email}' is already in use.", "Identity.EmailInUse");

    public static BaseException UsernameAlreadyUsedException(string username) 
        => new BadRequestException($"Username '{username}' is already in use", "Identity.UsernameInUse");

    public static BaseException InvalidEmailProviderException(string emailProvider)
        => new BadRequestException($"'{emailProvider}' is not valid email provider.", "Identity.InvalidEmailProvider");

    public static BaseException InvalidEmailConfirmationToken() =>
        new BadRequestException("Email confirmation token is invalid", "Identity.InvalidEmailConfirmationToken");

    public static BaseException InvalidCredentialsException() =>
        new UnauthorizedException("Invalid credentials.", "Identity.InvalidCredentials");
}
