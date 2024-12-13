using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.App.Services;
using Unity.Publisher.Tool.Endpoints.Requests;

namespace Unity.Publisher.Tool.Endpoints;

public static class EventsNotificationEndpoint
{
    public static WebApplication AddNotificationEndpoints(this WebApplication webApplication)
    {
        return webApplication.PostNotification();
    }

    private static WebApplication PostSubscription(this WebApplication webApplication)
    {
        webApplication.MapPost("start/notification", async (
            PostStartNotificationRequest startNotificationRequest,
            PublisherNotificationScheduler publisherNotificationScheduler,
            CancellationToken cancellationToken) =>
        {
            await publisherNotificationScheduler.ScheduleAsync(
                events: startNotificationRequest.Events,
                data: new DataTransferEndpoints(
                    startNotificationRequest.Sender,
                    startNotificationRequest.Receiver));
        });

        return webApplication;
    }

    private static WebApplication PostNotification(this WebApplication webApplication)
    {
        webApplication.MapPost("stop/notification", async (
            PostStopNotificationRequest stopNotificationRequest,
            PublisherNotificationScheduler publisherNotificationScheduler,
            CancellationToken cancellationToken) =>
        {
            await publisherNotificationScheduler.UnscheduleAsync(
                events: stopNotificationRequest.Events);
        });

        return webApplication;
    }
}
