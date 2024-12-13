using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.Infrastructure.Notification.Emails.Models;

public class EmailNotification : INotification<EmailContent>
{
    public required Sender Sender { get; init; }

    public required Receiver Recipient { get; init; }

    public required EmailContent Content { get; init; }

    public EmailServer EmailServer { get; init; } = EmailServer.Gmail;
}
