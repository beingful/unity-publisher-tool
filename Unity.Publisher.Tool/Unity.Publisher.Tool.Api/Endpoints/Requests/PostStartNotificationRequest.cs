using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.Endpoints.Requests;

public class PostStartNotificationRequest
{
    public required PublisherEvent[] Events;

    public required Sender Sender { get; init; }

    public required Receiver Receiver { get; init; }
}
