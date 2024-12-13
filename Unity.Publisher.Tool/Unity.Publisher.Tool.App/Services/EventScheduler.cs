using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.App.Services;

public class EventScheduler<TEvent, TEventInititor>
    where TEvent : PublisherEvents.Event, new()
    where TEventInititor : IScheduleWorker
{
    private readonly IScheduler _scheduler;

    public EventScheduler(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public TEvent Event => new();

    public void Schedule(TriggerTime triggerTime)
    {
        if (_scheduler.CanSchedule(Event.Name))
        {
            _scheduler.Schedule<TEventInititor>(
                new Job(id: Event.Name, time: triggerTime));
        }
        else
        {
            throw new Exception($"{Event.DisplayName} can not be scheduled.");
        }
    }

    public void Unschedule()
    {
        if (_scheduler.CanUnschedule(Event.Name))
        {
            _scheduler.Unschedule(Event.Name);
        }
        else
        {
            throw new Exception($"{Event.DisplayName} can not be unscheduled.");
        }
    }
}
