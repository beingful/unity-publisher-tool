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

    public ActionResult[] StartNotifications(PublisherEvent[] events, NotificationDetails data)
    {
        return ApplyToAll(events, subscriber => subscriber.Subscribe(data));
    }

    public ActionResult[] StopNotifictions(PublisherEvent[] events)
    {
        return ApplyToAll(events, subscriber => subscriber.Unsubscribe());
    }

    private ActionResult[] ApplyToAll(PublisherEvent[] events, Action<IPublisherEventNotificationSubscriber> subscriberAction)
    {
        ActionResult[] subscriberActionResults = new ActionResult[events.Length];

        for (int i = 0; i < events.Length; ++i)
        {
            PublisherEvent pulisherEvent = events[i];

            IPublisherEventNotificationSubscriber subscriber = _sbscribers.Provide(pulisherEvent);

            try
            {
                subscriberAction(subscriber);

                subscriberActionResults[i] = ActionResult.OnSuccess(pulisherEvent);
            }
            catch (Exception exception)
            {
                subscriberActionResults[i] = ActionResult.OnFail(pulisherEvent, exception.Message);
            }
        }

        return subscriberActionResults;
    }
}
