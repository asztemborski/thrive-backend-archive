namespace Thrive.Modules.Identity.Domain.Entities;

public sealed class RefreshToken
{
    public required string Token { get; init; }
    public required DateTime ExpiresAt { get; init; }
    public required DateTime CreatedAt { get; init; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
}