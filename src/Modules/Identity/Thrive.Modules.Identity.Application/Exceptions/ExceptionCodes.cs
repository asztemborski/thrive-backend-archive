namespace Thrive.Modules.Identity.Application.Exceptions;

internal static class ExceptionCodes
{
    public const string EmailAlreadyUsed = "Identity.EmailAlreadyUsed";
    public const string UsernameAlreadyUsed = "Identity.UsernameAlreadyUsed";
    public const string InvalidEmailProvider = "Identity.InvalidEmailProvider";
    public const string InvalidEmailConfirmationToken = "Identity.InvalidEmailConfirmationToken";
    public const string InvalidCredentials = "Identity.InvalidCredentials";
    public const string Unauthorized = "Identity.Unauthorized";
}