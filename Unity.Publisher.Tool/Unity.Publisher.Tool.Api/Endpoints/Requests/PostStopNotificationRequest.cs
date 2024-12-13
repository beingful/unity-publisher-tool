using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.Endpoints.Requests;

public class PostStopNotificationRequest
{
    public required PublisherEvent[] Events;
}
