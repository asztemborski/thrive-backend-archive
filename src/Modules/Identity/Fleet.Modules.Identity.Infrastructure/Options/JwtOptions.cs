namespace Fleet.Modules.Identity.Infrastructure.Options;

public sealed record JwtOptions
{
    public string SecretKey { get; init; } = string.Empty;
    public int TokenExpirationInMinutes { get; init; }
    public int RefreshTokenExpirationInDays { get; init; }
    public bool ValidateIssuerSigningKey { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateAudience { get; init; }
    public bool ValidateLifetime { get; init; }
}