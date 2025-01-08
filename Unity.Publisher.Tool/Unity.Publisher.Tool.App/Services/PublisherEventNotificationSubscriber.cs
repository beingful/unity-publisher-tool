using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.App.Services;

public abstract class PublisherEventNotificationSubscriber : IPublisherEventNotificationSubscriber
{
    private readonly IPublisherEventIdProvider _jobIdProvider;
    private readonly IScheduler _scheduler;

    public PublisherEventNotificationSubscriber(IPublisherEventIdProvider jobIdProvider, IScheduler scheduler)
    {
        _jobIdProvider = jobIdProvider;
        _scheduler = scheduler;
    }

    public abstract void Subscribe(NotificationDetails data);

    public void Unsubscribe()
    {
        string jobId = _jobIdProvider.Provide();

        _scheduler.Unschedule(jobId);
    }

    protected void Schedule<TScheduleWorker>(TriggerTime triggerTime, NotificationDetails notificationDetails)
        where TScheduleWorker : IScheduleWorker<NotificationDetails>
    {
        string jobId = _jobIdProvider.Provide();

        Job job = new(jobId, triggerTime);

        _scheduler.Schedule<TScheduleWorker, NotificationDetails>(job, notificationDetails);
    }
}
