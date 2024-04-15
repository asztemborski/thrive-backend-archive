using Thrive.Modules.Identity.Domain.Exceptions;
using Thrive.Modules.Identity.Domain.ValueObjects;
using Thrive.Shared.Abstractions.Domain;

namespace Thrive.Modules.Identity.Domain.Entities;

public sealed class IdentityUser : AggregateRoot<Guid>
{
    public UserEmail Email { get; private set; } = null!;
    public Username Username { get; private set; } = null!;
    public string Password { get; private set; }
    public bool IsActive { get; private set; } = true;
    public List<RefreshToken> RefreshTokens { get; } = [];

    public IdentityUser(Email email, Username username, string password) : this(password)
    {
        Email = new UserEmail(email);
        Username = username;
    }

    private IdentityUser(string password)
    {
        Password = password;
    }

    public void ToggleActivationStatus(bool value)
    {
        if (value == IsActive)
        {
            throw IsActive ? new UserActiveException() : new UserInactiveException();
        }

        IsActive = value;
    }

    public void AddRefreshToken(RefreshToken token)
    {
        RefreshTokens.Add(token);
    }

    public void RevokeRefreshToken(RefreshToken token)
    {
        RefreshTokens.Remove(token);
    }
}