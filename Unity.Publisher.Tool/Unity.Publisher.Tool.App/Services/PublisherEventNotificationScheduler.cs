using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.App.Services;

public abstract class PublisherEventNotificationScheduler : IPublisherEventNotificationScheduler
{
    private readonly IDataDrivenScheduler _scheduler;

    public PublisherEventNotificationScheduler(IDataDrivenScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public abstract Task ScheduleAsync(NotificationJobData schedulerData);

    public abstract Task UnscheduleAsync();

    protected async Task ScheduleAsync<TData>(Job job, NotificationJobData data) where TData : class
    {
        await _scheduler.ScheduleAsync<IScheduleWorker<TData>, NotificationJobData>(job, data);
    }

    protected async Task UnscheduleAsync(string jobId)
    {
        await _scheduler.UnscheduleAsync<NotificationJobData>(jobId);
    }
}
