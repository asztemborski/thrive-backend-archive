namespace Fleet.Modules.Notifications.Core.Mailing;

internal sealed record EmailTemplate
{
    public required string Subject { get; init; }
    public required string TextContent { get; init; }
    public required string HtmlContent { get; init; }
}