namespace Fleet.Modules.Identity.Infrastructure.Options;

internal sealed record GoogleAuthOptions
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
}