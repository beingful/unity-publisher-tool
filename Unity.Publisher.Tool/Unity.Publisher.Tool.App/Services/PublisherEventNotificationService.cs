using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherEventNotificationService
{
    private readonly IKeyedProvider<PublisherEvent, IPublisherEventNotificationSubscriber> _sbscribers;

    public PublisherEventNotificationService(IKeyedProvider<PublisherEvent, IPublisherEventNotificationSubscriber> subscribers)
    {
        _sbscribers = subscribers;
    }

    public void StartNotifications(PublisherEvent[] events, NotificationDetails data)
    {
        foreach (PublisherEvent publisherEvent in events)
        {
            IPublisherEventNotificationSubscriber scheduler = _sbscribers.Provide(publisherEvent);

            scheduler.Subscribe(data);
        }
    }

    public void StopNotifictions(PublisherEvent[] events)
    {
        foreach (PublisherEvent publisherEvent in events)
        {
            IPublisherEventNotificationSubscriber scheduler = _sbscribers.Provide(publisherEvent);

            scheduler.Unsubscribe();
        }
    }
}
