namespace Unity.Publisher.Tool.Domain.Notifications;

public interface INotificator<TNotification> where TNotification : INotification<Content>
{
    public Task SendAsync(TNotification notification, CancellationToken cancellationToken = default);
}

public interface INotificator
{
    public Task SendAsync(Sender sender, Receiver receiver, Message message, CancellationToken cancellationToken = default);
}
