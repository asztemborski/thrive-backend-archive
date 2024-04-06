using Fleet.Modules.Identity.Domain.Exceptions;

namespace Fleet.Modules.Identity.Domain.ValueObjects;

public sealed record Username
{
    public string Value { get; }

    private Username(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidUsernameException();
        }

        if (value.Length is < 1 or > 100)
        {
            throw new InvalidUsernameException();
        }

        Value = value;
    }

    public static implicit operator string(Username username) => username.Value;

    public static implicit operator Username(string username) => new(username);
}