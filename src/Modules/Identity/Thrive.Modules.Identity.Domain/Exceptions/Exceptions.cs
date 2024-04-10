using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Identity.Domain.Exceptions;

internal static class DomainExceptions
{
    public static BaseException EmailAlreadyConfirmedException() => new("Email is already confirmed.", "Identity.EmailConfirmed");
    public static BaseException InvalidEmailException() => new("Email address is not valid.", "Identity.InvalidEmail");
    public static BaseException InvalidUsernameException() => new("Username is not valid.", "Identity.InvalidUsername");
    public static BaseException SameEmailException() => 
        new("Cannot update email to the current account email address.", "Identity.SameEmails");

    public static BaseException UserActiveException() => new("User is active.", "Identity.UserActive");
    public static BaseException UserInactiveException() => new("User is inactive.", "Identity.Inactive");
}
