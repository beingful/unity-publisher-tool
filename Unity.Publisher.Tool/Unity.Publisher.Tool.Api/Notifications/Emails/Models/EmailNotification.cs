namespace Unity.Publisher.Tool.Notifications.Emails;

public class EmailNotification
{
    public required Sender Sender { get; init; }

    public required Recipient Recipient { get; init; }

    public required EmailContent Content { get; init; }

    public EmailServer EmailServer { get; init; } = EmailServer.Gmail;
}
