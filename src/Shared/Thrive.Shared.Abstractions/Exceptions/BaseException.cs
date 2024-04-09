using System.Net;

namespace Thrive.Shared.Abstractions.Exceptions;

public abstract class BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    : Exception(message)
{
    protected BaseException(string message, IEnumerable<string> errors,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest) : this(message, statusCode)
    {
        Errors.AddRange(errors.Select(err => new Error(string.Empty, string.Empty, err)));
    }

    protected BaseException(string message, IEnumerable<Error> errors,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest) : this(message, statusCode)
    {
        Errors.AddRange(errors);
    }

    public List<Error> Errors { get; } = new();
    public HttpStatusCode StatusCode { get; } = statusCode;
}