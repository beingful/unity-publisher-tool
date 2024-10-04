namespace Unity.Publisher.Tool.Notifications;

public class Notification
{
    public required Sender Sender { get; init; }

    public required Recipient Recipient { get; init; }

    public required Content Content { get; init; }
}
