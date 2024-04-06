namespace Fleet.Modules.Identity.Infrastructure.Options;

internal sealed record GoogleAuthOptions
{
    public string ClientId { get; init; } = string.Empty;
    public string ClientSecret { get; init; } = string.Empty;
}