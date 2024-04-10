using System.Net;

namespace Thrive.Shared.Abstractions.Exceptions;

public class UnauthorizedException(string message) : BaseException(message, HttpStatusCode.Unauthorized);