using System.Net;

namespace Thrive.Shared.Abstractions.Exceptions;

public class BaseException : Exception
{
    protected BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        StatusCode = statusCode;
    }
    
    protected BaseException(string message, IEnumerable<Error> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : this(message, statusCode)
    {
        Errors.AddRange(errors);
    }

    protected BaseException(string message, string code, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : this(message, statusCode)
    {
        Code = code;
    }
    
    public List<Error> Errors { get; } = new();
    public HttpStatusCode StatusCode { get; }
    public string Code { get; init; } = "Thrive.Exception";
}