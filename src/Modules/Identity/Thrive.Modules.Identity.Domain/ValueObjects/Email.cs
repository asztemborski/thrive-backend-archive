using System.ComponentModel.DataAnnotations;
using Thrive.Modules.Identity.Domain.Exceptions;

namespace Thrive.Modules.Identity.Domain.ValueObjects;

public sealed record Email
{
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

        if (new EmailAddressAttribute().IsValid(value) is false)
        {
            throw new InvalidEmailException();
        }
        
        Value = value;
    }

    public static implicit operator string(Email userEmail) => userEmail.Value;

    public static implicit operator Email(string email) => new(email);
}