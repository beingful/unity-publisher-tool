using Unity.Publisher.Tool.Endpoints.Requests;
using Unity.Publisher.Tool.Services;

namespace Unity.Publisher.Tool.Endpoints;

public static class EventNotificationEndpoint
{
    public static WebApplication AddNotificationEndpoints(this WebApplication webApplication)
    {
        return webApplication.PostNotification();
    }

    private static WebApplication PostNotification(this WebApplication webApplication)
    {
        webApplication.MapPost("schedule/notifications", async (
            PostNotificationRequestModel notificationRequest,
            NotificationService notificationService,
            CancellationToken cancellationToken) =>
        {
            await notificationService.NotifyAsync(notificationRequest.Sender,
                notificationRequest.Recipient, cancellationToken);
        });

        return webApplication;
    }
}
