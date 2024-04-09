using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Notifications.Core.Exceptions;

internal sealed class EmptyEmailConfirmationUri() : BaseException("Email confirmation uri cannot be empty");