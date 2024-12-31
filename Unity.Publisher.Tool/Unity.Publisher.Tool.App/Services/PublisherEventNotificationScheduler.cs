using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

namespace Unity.Publisher.Tool.App.Services;

public abstract class PublisherEventNotificationScheduler : INotificationScheduler
{
    private readonly IPublisherEventIdProvider _publisherEventIdProvider;
    private readonly IScheduler _scheduler;

    public PublisherEventNotificationScheduler(
        IPublisherEventIdProvider publisherEventIdProvider, IScheduler scheduler)
    {
        _publisherEventIdProvider = publisherEventIdProvider;
        _scheduler = scheduler;
    }

    public abstract void Schedule(NotificationDetails schedulerData);

    public void Unschedule()
    {
        _scheduler.Unschedule(_publisherEventIdProvider.Provide());
    }

    protected void Schedule<TPublisherEventData>(TriggerTime trigger, NotificationDetails data)
        where TPublisherEventData : class
    {
        Job job = new(_publisherEventIdProvider.Provide(), trigger);

        _scheduler.Schedule<PublisherEventHandler<TPublisherEventData>, NotificationDetails>(job, data);
    }
}
