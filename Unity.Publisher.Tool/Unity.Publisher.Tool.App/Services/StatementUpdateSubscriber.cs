using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Publisher.Services;

namespace Unity.Publisher.Tool.App.Services;

public class StatementUpdateSubscriber : PublisherEventNotificationSubscriber
{
    public StatementUpdateSubscriber(
        IPublisherEventIdProvider publisherEventIdProvider,
        IScheduler scheduler) : base(publisherEventIdProvider, scheduler)
    {
    }

    public override void Subscribe(NotificationDetails notificationDetails)
    {
        Schedule<StatementUpdatePerformer>(TriggerTime.FromTimeInterval(minutes: 1), notificationDetails);
    }
}
