using Unity.Publisher.Tool.Domain.Notifications;
using Unity.Publisher.Tool.Domain.Publisher;

namespace Unity.Publisher.Tool.Endpoints.Requests;

public class PostStartNotificationRequest
{
    public required PublisherEvent[] Events { get; init; }

    public required PublisherEvent Event { get; init; }

    public required Sender Sender { get; init; }

    public required Receiver Receiver { get; init; }
}
