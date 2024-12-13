using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.Infrastructure.Db.Entities;

public class NotificationEndpointsEntity : BaseEntity
{
    public override required string Id { get; init; }

    public required Sender Sender { get; init; }

    public required Receiver Receiver { get; init; }
}
