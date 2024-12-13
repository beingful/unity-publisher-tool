using Unity.Publisher.Tool.App.Models;
using static Unity.Publisher.Tool.App.Models.PublisherEvents;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherNotificationScheduler
{
    private readonly Dictionary<PublisherEvent, IEventNotificationScheduler> _eventNotificationSchedulers;

    public PublisherNotificationScheduler(
        IEventNotificationScheduler<StatementUpdate> statementUpdateScheduler,
        IEventNotificationScheduler<MonthlyReport> monthlyReportScheduler)
    {
        _eventNotificationSchedulers = new Dictionary<PublisherEvent, IEventNotificationScheduler>()
        {
            { PublisherEvent.StatementUpdate, statementUpdateScheduler },
            { PublisherEvent.MonthlyReport, monthlyReportScheduler }
        };
    }

    public async Task ScheduleAsync(PublisherEvent[] events, DataTransferEndpoints data)
    {
        Task[] scheduleTasks = new Task[events.Length];

        for (int i = 0; i < scheduleTasks.Length; ++i)
        {
            scheduleTasks[i] = _eventNotificationSchedulers[events[i]]
                .ScheduleAsync(data);
        }

        await Task.WhenAll(scheduleTasks);
    }

    public async Task UnscheduleAsync(PublisherEvent[] events)
    {
        Task[] unscheduleTasks = new Task[events.Length];

        for (int i = 0; i < unscheduleTasks.Length; ++i)
        {
            unscheduleTasks[i] = _eventNotificationSchedulers[events[i]]
                .UnscheduleAsync();
        }

        await Task.WhenAll(unscheduleTasks);
    }
}
