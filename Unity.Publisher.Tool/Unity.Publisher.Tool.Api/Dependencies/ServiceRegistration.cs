using Unity.Publisher.Tool.Notifications.Emails;
using Unity.Publisher.Tool.Notifications.Intefaces;
using Unity.Publisher.Tool.Services;

namespace Unity.Publisher.Tool.Dependencies;

public static class ServiceRegistration
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<NotificationService>()
            .AddScoped<INotificatorApiService, EmailNotificatorApiService>();
    }
}
