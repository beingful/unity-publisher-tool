using Unity.Publisher.Tool.Notifications;

namespace Unity.Publisher.Tool.Endpoints.Requests;

public class PostNotificationRequestModel
{
    public required Sender Sender { get; init; }

    public required Recipient Recipient { get; init; }
}
