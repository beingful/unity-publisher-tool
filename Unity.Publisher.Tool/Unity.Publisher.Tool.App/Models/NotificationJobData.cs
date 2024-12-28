using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.App.Models;

public class NotificationJobData
{
    public readonly Sender Sender;
    public readonly Receiver Receiver;

    public NotificationJobData(Sender sender, Receiver receiver)
    {
        Sender = sender;
        Receiver = receiver;
    }
}
