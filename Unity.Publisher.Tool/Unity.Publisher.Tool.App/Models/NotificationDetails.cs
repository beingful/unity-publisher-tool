using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.App.Models;

public class NotificationDetails
{
    public readonly Sender Sender;
    public readonly Receiver Receiver;

    public NotificationDetails(Sender sender, Receiver receiver)
    {
        Sender = sender;
        Receiver = receiver;
    }
}
