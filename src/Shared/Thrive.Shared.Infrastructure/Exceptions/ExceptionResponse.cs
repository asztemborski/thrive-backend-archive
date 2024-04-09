using System.Net;
using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Shared.Infrastructure.Exceptions;

public sealed record ExceptionResponse
{
    public required string Title { get; set; }
    public List<Error> Errors { get; } = new();
    public string Source { get; set; } = string.Empty;
    public HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
}