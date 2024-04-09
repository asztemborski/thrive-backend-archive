using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Identity.Domain.Exceptions;

internal sealed class EmailAlreadyConfirmedException() : BaseException("Email is already confirmed.");

internal sealed class InvalidEmailException() : BaseException("Email address is not valid.");

internal sealed class InvalidUsernameException() : BaseException("Username is not valid.");

internal sealed class SameEmailException()
    : BaseException("Cannot update email to the current account email address.");

internal sealed class UserActiveException() : BaseException("User is active.");

internal sealed class UserInactiveException() : BaseException("User is inactive.");