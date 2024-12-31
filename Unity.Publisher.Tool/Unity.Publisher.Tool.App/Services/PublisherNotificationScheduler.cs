using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherNotificationScheduler
{
    private readonly IKeyedProvider<PublisherEvent, INotificationScheduler> _schedulers;

    public PublisherNotificationScheduler(IKeyedProvider<PublisherEvent, INotificationScheduler> schedulers)
    {
        _schedulers = schedulers;
    }

    public void Schedule(PublisherEvent[] events, NotificationDetails data)
    {
        foreach (PublisherEvent publisherEvent in events)
        {
            INotificationScheduler scheduler = _schedulers.Provide(publisherEvent);

            scheduler.Schedule(data);
        }
    }

    public void Unschedule(PublisherEvent[] events)
    {
        foreach (PublisherEvent publisherEvent in events)
        {
            INotificationScheduler scheduler = _schedulers.Provide(publisherEvent);

            scheduler.Unschedule();
        }
    }
}
