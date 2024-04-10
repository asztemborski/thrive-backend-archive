using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Identity.Domain.Exceptions;

internal static class DomainExceptions
{
    public static BaseException EmailAlreadyConfirmedException() => new("Email is already confirmed.");
    public static BaseException InvalidEmailException() => new("Email address is not valid.");
    public static BaseException InvalidUsernameException() => new("Username is not valid.");
    public static BaseException SameEmailException() => 
        new("Cannot update email to the current account email address.");

    public static BaseException UserActiveException() => new("User is active.");
    public static BaseException UserInactiveException() => new("User is inactive.");
}
