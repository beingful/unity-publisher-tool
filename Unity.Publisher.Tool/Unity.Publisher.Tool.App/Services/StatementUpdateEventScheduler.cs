using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Domain.Business.Models;

namespace Unity.Publisher.Tool.App.Services;

public class StatementUpdateEventScheduler : PublisherEventNotificationScheduler
{
    public StatementUpdateEventScheduler(
        IPublisherEventIdProvider publisherEventIdProvider, IScheduler scheduler)
        : base(publisherEventIdProvider, scheduler)
    {
    }

    public override void Schedule(NotificationDetails data)
    {
        Schedule<PublisherReport>(TriggerTime.FromTimeInterval(minutes: 1), data);
    }
}
