using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

namespace Unity.Publisher.Tool.App.Services;

public class MonthlyReportEventScheduler : PublisherEventNotificationScheduler
{
    public MonthlyReportEventScheduler(
        IPublisherEventIdProvider publisherEventIdProvider, IScheduler scheduler)
        : base(publisherEventIdProvider, scheduler)
    {
    }

    public override void Schedule(NotificationDetails data)
    {
        Schedule<PublisherReport>(TriggerTime.Monthly(), data);
    }
}
