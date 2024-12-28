using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.App.Services;

public interface IPublisherEventNotificationScheduler
{
    Task ScheduleAsync(NotificationJobData schedulerData);

    Task UnscheduleAsync();
}
