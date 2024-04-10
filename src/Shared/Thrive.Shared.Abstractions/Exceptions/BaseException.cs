using System.Net;

namespace Thrive.Shared.Abstractions.Exceptions;

public class BaseException : Exception
{
    public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseException(string message, IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : this(message, statusCode)
    {
        foreach (var err in errors)
        {
            Errors.Add(new Error(string.Empty, string.Empty, err));
        }
    }

    public BaseException(string message, IEnumerable<Error> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : this(message, statusCode)
    {
        Errors.AddRange(errors);
    }

    public BaseException(string message, string code, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : this(message, statusCode)
    {
        Code = code;
    }

    public List<Error> Errors { get; } = new();
    public HttpStatusCode StatusCode { get; }
    public string? Code { get; init; }
}