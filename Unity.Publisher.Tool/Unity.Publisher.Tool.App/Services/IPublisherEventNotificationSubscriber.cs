using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.App.Services;

public interface IPublisherEventNotificationSubscriber
{
    void Subscribe(NotificationDetails notificationDetails);

    void Unsubscribe();
}
