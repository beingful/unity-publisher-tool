using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
using Unity.Publisher.Tool.Infrastructure.Db;
using Unity.Publisher.Tool.Infrastructure.Db.Entities;
using Unity.Publisher.Tool.Infrastructure.Notification;
using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.App.Services;

public class EventNotificationHandler<TData> : IEventHandler<TData>
{
    private readonly IDocumentBuilder<TData> _documentBuilder;
    private readonly INotificator _notificator;
    private readonly IStorage _storage;

    public EventNotificationHandler(
        IDocumentBuilder<TData> documentBuilder,
        INotificator notificator,
        IStorage storage)
    {
        _documentBuilder = documentBuilder;
        _notificator = notificator;
        _storage = storage;
    }

    public PublisherEvents.Event Event => PublisherEvents.ResolveFor<TData>();

    public async Task HandleAsync(TData content)
    {
        NotificationEndpointsEntity notificationEndpoints =
            await _storage.GetAsync<NotificationEndpointsEntity>(id: Event.Name);

        Message message = new()
        {
            Subject = $"Unity asset store publisher event: {Event.DisplayName}",
            Body = _documentBuilder.Build(content)
        };

        await _notificator
            .SendAsync(notificationEndpoints.Sender, notificationEndpoints.Receiver, message, CancellationToken.None)
            .ConfigureAwait(false);
    }
}
