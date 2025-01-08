using Unity.Publisher.Tool.Domain.Publisher;

namespace Unity.Publisher.Tool.Endpoints.Requests;

public class PostStopNotificationRequest
{
    public required PublisherEvent[] Events { get; init; }
}
