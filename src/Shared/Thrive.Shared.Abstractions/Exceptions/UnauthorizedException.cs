using System.Net;

namespace Thrive.Shared.Abstractions.Exceptions;

public abstract class UnauthorizedException(string message)
    : BaseException(message, HttpStatusCode.Unauthorized);