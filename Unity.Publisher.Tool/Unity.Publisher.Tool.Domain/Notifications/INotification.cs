namespace Unity.Publisher.Tool.Domain.Notifications;

public interface INotification<out TContent> where TContent : Content
{
    public Sender Sender { get; }

    public Receiver Recipient { get; }

    public TContent Content { get; }
}
