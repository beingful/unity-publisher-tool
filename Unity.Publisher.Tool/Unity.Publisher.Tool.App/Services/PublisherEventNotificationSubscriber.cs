using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.App.Services;

public abstract class PublisherEventNotificationSubscriber : IPublisherEventNotificationSubscriber
{
    private readonly IPublisherEventIdProvider _idProvider;
    private readonly IScheduler _scheduler;

    public PublisherEventNotificationSubscriber(IPublisherEventIdProvider idProvider, IScheduler scheduler)
    {
        _idProvider = idProvider;
        _scheduler = scheduler;
    }

    public abstract void Subscribe(NotificationDetails data);

    public void Unsubscribe()
    {
        string jobId = _idProvider.Provide();

        if (_scheduler.CanUnschedule(jobId))
        {
            _scheduler.Unschedule(jobId);
        }
    }

    protected void Schedule<TScheduleWorker>(TriggerTime trigger, NotificationDetails notificationDetails)
        where TScheduleWorker : IScheduleWorker<NotificationDetails>
    {
        string jobId = _idProvider.Provide();

        if (_scheduler.CanSchedule(jobId))
        {
            _scheduler.Schedule<TScheduleWorker, NotificationDetails>(
                job: new Job(jobId, trigger),
                workerData: new NotificationDetails(notificationDetails.Sender, notificationDetails.Receiver));
        }
    }
}
