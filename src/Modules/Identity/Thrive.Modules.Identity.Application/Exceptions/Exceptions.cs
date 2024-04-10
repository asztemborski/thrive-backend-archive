using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Identity.Application.Exceptions;

internal static class ApplicationExceptions
{
    public static BaseException EmailAlreadyUsedException(string email)
         => new BadRequestException($"Email {email} is already in use.");

    public static BaseException UsernameAlreadyUsedException(string username) 
        => new BadRequestException($"Username {username} is already in use");
    
    public static BaseException InvalidEmailProviderException(string emailProvider)
        => new BadRequestException($"{emailProvider} is not valid email provider.");

    public static BaseException InvalidEmailConfirmationToken() =>
        new BadRequestException("Email confirmation token is invalid");

    public static BaseException InvalidCredentialsException() =>
        new UnauthorizedException("Invalid credentials.");
}
