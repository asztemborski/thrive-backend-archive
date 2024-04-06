namespace Fleet.Modules.Identity.Application.DTOs;

public sealed record Tokens(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);