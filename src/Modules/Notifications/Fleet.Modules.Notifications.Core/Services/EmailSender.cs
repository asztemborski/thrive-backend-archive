using Fleet.Modules.Notifications.Core.Contracts;
using Fleet.Modules.Notifications.Core.Mailing;
using Fleet.Modules.Notifications.Core.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Fleet.Modules.Notifications.Core.Services;

internal sealed class EmailSender : IEmailSender
{
    private readonly MailingOptions _mailingOptions;

    public EmailSender(IOptions<MailingOptions> mailingOptions) => _mailingOptions = mailingOptions.Value;

    public async Task SendAsync(string receiver, EmailTemplate emailTemplate,
        CancellationToken cancellationToken)
    {
        var mail = new MimeMessage
        {
            From = { new MailboxAddress(_mailingOptions.DisplayName, _mailingOptions.Mail) },
            To = { MailboxAddress.Parse(receiver) },
            Subject = emailTemplate.Subject,
            Body = new BodyBuilder
            {
                HtmlBody = emailTemplate.HtmlContent,
                TextBody = emailTemplate.TextContent
            }.ToMessageBody()
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_mailingOptions.Host, _mailingOptions.Port, SecureSocketOptions.StartTls,
            cancellationToken);

        await client.AuthenticateAsync(_mailingOptions.Username, _mailingOptions.Password, cancellationToken);
        await client.SendAsync(mail, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}