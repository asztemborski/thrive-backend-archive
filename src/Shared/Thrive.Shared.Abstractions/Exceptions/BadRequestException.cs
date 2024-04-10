namespace Thrive.Shared.Abstractions.Exceptions;

public class BadRequestException(string message) : BaseException(message);