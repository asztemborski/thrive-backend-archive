using System.Net;

namespace Thrive.Shared.Abstractions.Exceptions;

public abstract class BadRequestException(string message)
    : BaseException(message, HttpStatusCode.BadRequest);