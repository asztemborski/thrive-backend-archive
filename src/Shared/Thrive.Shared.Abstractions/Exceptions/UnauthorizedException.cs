using System.Net;

namespace Thrive.Shared.Abstractions.Exceptions;

public class UnauthorizedException(string message, string? code = default) 
    : BaseException(message, code, HttpStatusCode.Unauthorized);