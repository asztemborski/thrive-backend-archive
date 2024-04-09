namespace Thrive.Modules.Notifications.Core.Options;

internal sealed record MailingOptions
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required string Mail { get; init; }
    public required string DisplayName { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}