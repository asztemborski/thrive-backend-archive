namespace Thrive.Modules.Identity.Domain.Entities;

public sealed class EmailConfirmationToken
{
    public string Email { get; private init; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public EmailConfirmationToken(string email, string token, DateTime expiresAt)
    {
        Email = email;
        Token = token;
        ExpiresAt = expiresAt;
    }

    public void UpdateToken(EmailConfirmationToken token)
    {
        Token = token.Token;
        ExpiresAt = token.ExpiresAt;
    }
}