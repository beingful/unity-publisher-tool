using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.App.Services;

public interface INotificationScheduler
{
    void Schedule(NotificationDetails schedulerData);

    void Unschedule();
}
