namespace Fleet.Shared.Abstractions.Exceptions;

public class ValidationException(IEnumerable<Error> errors)
    : BaseException("Some validation errors have occured", errors);