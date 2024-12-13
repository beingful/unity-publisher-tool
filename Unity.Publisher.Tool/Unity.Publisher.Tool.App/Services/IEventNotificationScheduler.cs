using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.App.Services;

public interface IEventNotificationScheduler
{
    public Task ScheduleAsync(DataTransferEndpoints data);

    public Task UnscheduleAsync();
}

public interface IEventNotificationScheduler<TEvent> : IEventNotificationScheduler
    where TEvent : PublisherEvents.Event;
