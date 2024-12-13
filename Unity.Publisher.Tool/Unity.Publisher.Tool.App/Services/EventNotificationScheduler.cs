using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Db;
using Unity.Publisher.Tool.Infrastructure.Db.Entities;
using Unity.Publisher.Tool.Infrastructure.Notification.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

namespace Unity.Publisher.Tool.App.Services;

public abstract class EventNotificationScheduler<TEvent, TEventInitiator> : IEventNotificationScheduler<TEvent>
    where TEvent : PublisherEvents.Event, new()
    where TEventInitiator : IScheduleWorker
{
    private readonly EventScheduler<TEvent, TEventInitiator> _scheduler;
    private readonly IStorage _storage;

    public EventNotificationScheduler(
        EventScheduler<TEvent, TEventInitiator> scheduler,
        IStorage storage)
    {
        _scheduler = scheduler;
        _storage = storage;
    }

    public abstract Task ScheduleAsync(DataTransferEndpoints data);

    public virtual async Task UnscheduleAsync()
    {
        _scheduler.Unschedule();

        await RemoveTransferDetailsAsync();
    }

    protected async Task ScheduleAsync(TriggerTime triggerTime, Sender sender, Receiver receiver)
    {
        await SaveTransferDetailsAsync(sender, receiver);

        try
        {
            _scheduler.Schedule(triggerTime);
        }
        catch
        {
            await RemoveTransferDetailsAsync();

            throw;
        }
    }

    private async Task SaveTransferDetailsAsync(Sender sender, Receiver receiver)
    {
        await _storage.InsertAsync(new NotificationEndpointsEntity()
        {
            Id = _scheduler.Event.Name,
            Sender = sender,
            Receiver = receiver
        });
    }

    private async Task RemoveTransferDetailsAsync()
    {
        await _storage.RemoveAsync<NotificationEndpointsEntity>(id: _scheduler.Event.Name);
    }
}
