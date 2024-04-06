namespace Fleet.Shared.Abstractions.Exceptions;

public sealed record Error(string Code, string Path, string Message);