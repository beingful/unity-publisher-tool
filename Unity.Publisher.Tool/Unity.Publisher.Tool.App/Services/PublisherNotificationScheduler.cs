using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherNotificationScheduler
{
    private readonly IKeyedProvider<PublisherEvent, IPublisherEventNotificationScheduler> _schedulers;

    public PublisherNotificationScheduler(IKeyedProvider<PublisherEvent, IPublisherEventNotificationScheduler> schedulers)
    {
        _schedulers = schedulers;
    }

    public async Task ScheduleAsync(PublisherEvent[] events, NotificationJobData data)
    {
        Task[] scheduleTasks = new Task[events.Length];

        for (int i = 0; i < scheduleTasks.Length; ++i)
        {
            IPublisherEventNotificationScheduler scheduler = _schedulers.Provide(events[i]);

            scheduleTasks[i] = scheduler.ScheduleAsync(data);
        }

        await Task.WhenAll(scheduleTasks);
    }

    public async Task UnscheduleAsync(PublisherEvent[] events)
    {
        Task[] unscheduleTasks = new Task[events.Length];

        for (int i = 0; i < unscheduleTasks.Length; ++i)
        {
            IPublisherEventNotificationScheduler scheduler = _schedulers.Provide(events[i]);

            unscheduleTasks[i] = scheduler.UnscheduleAsync();
        }

        await Task.WhenAll(unscheduleTasks);
    }
}
