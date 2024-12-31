using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.App.Services;
using Unity.Publisher.Tool.Endpoints.Requests;

namespace Unity.Publisher.Tool.Endpoints;

public static class EventsNotificationEndpoint
{
    public static IEndpointRouteBuilder AddNotificationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .PostStartNotification()
            .PostStopNotification();
    }

    private static IEndpointRouteBuilder PostStartNotification(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("start/notification", (
            PostStartNotificationRequest startNotificationRequest,
            PublisherNotificationScheduler publisherNotificationScheduler,
            CancellationToken cancellationToken) =>
        {
            publisherNotificationScheduler.Schedule(
                events: startNotificationRequest.Events,
                data: new NotificationDetails(
                    startNotificationRequest.Sender,
                    startNotificationRequest.Receiver));
        });

        return endpoints;
    }

    private static IEndpointRouteBuilder PostStopNotification(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("stop/notification", (
            PostStopNotificationRequest stopNotificationRequest,
            PublisherNotificationScheduler publisherNotificationScheduler,
            CancellationToken cancellationToken) =>
        {
            publisherNotificationScheduler.Unschedule(stopNotificationRequest.Events);
        });

        return endpoints;
    }
}
