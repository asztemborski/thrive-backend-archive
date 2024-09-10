namespace Thrive.Modules.Identity.Application.DTOs;

public sealed record Tokens(string AccessToken, string RefreshToken, DateTime ExpiresAt);