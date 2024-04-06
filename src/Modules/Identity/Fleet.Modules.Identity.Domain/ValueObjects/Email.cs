using System.Text.RegularExpressions;
using Fleet.Modules.Identity.Domain.Exceptions;

namespace Fleet.Modules.Identity.Domain.ValueObjects;

public sealed record Email
{
    private static readonly Regex Regex = new(
        """^(?(")(".+?(?<!\\)"@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))""" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.Compiled);

    public string Value { get; }

    private Email(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidEmailException();
        }

        if (value.Length is < 5 or > 100)
        {
            throw new InvalidEmailException();
        }

        value = value.ToLowerInvariant();


        Value = value;
    }

    public static implicit operator string(Email userEmail) => userEmail.Value;

    public static implicit operator Email(string email) => new(email);
}