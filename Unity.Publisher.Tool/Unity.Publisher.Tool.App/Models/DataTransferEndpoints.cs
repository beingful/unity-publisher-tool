using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.App.Models;

public class DataTransferEndpoints
{
    public readonly Sender Sender;
    public readonly Receiver Receiver;

    public DataTransferEndpoints(Sender sender, Receiver receiver)
    {
        Sender = sender;
        Receiver = receiver;
    }
}
