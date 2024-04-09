namespace Thrive.Modules.Notifications.Core.Mailing.TemplateBuilders;

internal sealed class EmailConfirmationTemplateBuilder : TemplateBuilder
{
    private readonly string _username;
    private readonly string _confirmationEmailUri;

    public EmailConfirmationTemplateBuilder(string username, string confirmationEmailUri)
    {
        _username = username;
        _confirmationEmailUri = confirmationEmailUri;
    }

    public EmailTemplate Build()
    {
        var textContent = ReadTemplateFile("EmailConfirmation/EmailConfirmation.txt");
        var htmlContent = ReadTemplateFile("EmailConfirmation/EmailConfirmation.html");

        return new EmailTemplate
        {
            Subject = "Verify Your Thrive Account",
            TextContent = string.Format(textContent, _username, _confirmationEmailUri),
            HtmlContent = string.Format(htmlContent, _username, _confirmationEmailUri)
        };
    }

    private static string ReadTemplateFile(string relativePath)
    {
        var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TemplatesDirectory, relativePath);
        return File.ReadAllText(fullPath);
    }
}