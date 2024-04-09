using Fleet.Shared.Abstractions.Exceptions;

namespace Fleet.Modules.Notifications.Core.Exceptions;

internal sealed class EmptyEmailConfirmationUri() : BaseException("Email confirmation uri cannot be empty");