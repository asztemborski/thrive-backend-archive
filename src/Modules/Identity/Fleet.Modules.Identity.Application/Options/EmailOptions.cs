namespace Fleet.Modules.Identity.Application.Options;

public sealed record EmailOptions
{
    public required int EmailConfirmationTokenExpirationTime { get; init; }
    public required string EmailConfirmationBaseUri { get; init; }
    public required List<string> BannedEmailProviders { get; init; }
}