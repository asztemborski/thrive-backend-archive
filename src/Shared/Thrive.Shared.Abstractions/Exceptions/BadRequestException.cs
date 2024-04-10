namespace Thrive.Shared.Abstractions.Exceptions;

public class BadRequestException(string message, string? code = default) : BaseException(message, code);