using System.Net;

namespace Fleet.Shared.Abstractions.Exceptions;

public abstract class UnauthorizedException(string message)
    : BaseException(message, HttpStatusCode.Unauthorized);