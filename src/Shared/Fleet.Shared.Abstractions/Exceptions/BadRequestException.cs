using System.Net;

namespace Fleet.Shared.Abstractions.Exceptions;

public abstract class BadRequestException(string message)
    : BaseException(message, HttpStatusCode.BadRequest);