using Unity.Publisher.Tool.Notifications;
using Unity.Publisher.Tool.Notifications.Intefaces;

namespace Unity.Publisher.Tool.Services;

public sealed class NotificationService
{
    private readonly INotificatorApiService _notificator;
    private readonly ILogger _logger;

    public NotificationService(
        INotificatorApiService notificator,
        ILogger<NotificationService> logger)
    {
        _notificator = notificator;
        _logger = logger;
    }

    public async Task NotifyAsync(Sender sender, Recipient recipient,
        CancellationToken cancellationToken)
    {
        Notification notification = new()
        {
            Sender = sender,
            Recipient = recipient,
            Content = new Content
            {
                Message = new Message
                {
                    Subject = "Test subject",
                    Body = "Test body"
                }
            }
        };

        await _notificator.SendAsync(notification);
    }
}
