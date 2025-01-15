using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Infrastructure.Scheduling;

namespace Unity.Publisher.Tool.App.Services;

public class MonthlyReportSubscriber : PublisherEventNotificationSubscriber
{
    public MonthlyReportSubscriber(
        IPublisherEventIdProvider publisherEventIdProvider,
        IScheduler scheduler) : base(publisherEventIdProvider, scheduler)
    {
    }

    public override void Subscribe(NotificationDetails notificationDetails)
    {
        Schedule<MonthlyReportPerformer>(TriggerTime.FromTimeInterval(1), notificationDetails);
    }
}
