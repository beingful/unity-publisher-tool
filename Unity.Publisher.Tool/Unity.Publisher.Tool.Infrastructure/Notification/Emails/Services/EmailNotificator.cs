using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.Infrastructure.Notification.Emails.Services;

public class EmailNotificator : INotificator<EmailNotification>
{
    private readonly SmtpServersCollection _smtpServers;

    public EmailNotificator(SmtpServersCollection smtpServers)
    {
        _smtpServers = smtpServers;
    }

    public Task SendAsync(EmailNotification notification, CancellationToken cancellationToken = default)
    {
        SmtpServer smtpServer = _smtpServers[notification.EmailServer];
        MimeMessage message = BuildMessage(notification);

        return SendAsync(message, smtpServer, notification.Sender.Account, cancellationToken);
    }

    private MimeMessage BuildMessage(INotification<EmailContent> notification)
    {
        MimeMessage message = new()
        {
            Subject = notification.Content.Message.Subject,
            Body = new TextPart(format: TextFormat.Plain)
            {
                Text = notification.Content.Message.Body
            }
        };

        message.From.Add(new MailboxAddress(
            name: notification.Sender.Name,
            address: notification.Sender.Account.Id));

        message.To.Add(new MailboxAddress(
            name: string.Empty,
            address: notification.Recipient.Address));

        return message;
    }

    private async Task SendAsync(MimeMessage message, SmtpServer smtpServer, Account senderAccount, CancellationToken cancellationToken)
    {
        using SmtpClient smtpClient = new();

        await smtpClient.ConnectAsync(smtpServer.Host, smtpServer.Port, cancellationToken: cancellationToken);

        await smtpClient.AuthenticateAsync(
            userName: senderAccount.Id,
            password: senderAccount.Secret,
            cancellationToken: cancellationToken);

        await smtpClient.SendAsync(message, cancellationToken);

        await smtpClient.DisconnectAsync(quit: true, cancellationToken);
    }
}
