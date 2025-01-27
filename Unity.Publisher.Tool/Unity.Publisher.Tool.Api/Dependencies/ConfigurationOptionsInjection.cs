using Unity.Publisher.Tool.Access.Options;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Options;

namespace Unity.Publisher.Tool.Dependencies;

public static class ConfigurationOptionsInjection
{
    public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<AuthorizationOptions>(
                configuration.GetSection(AuthorizationOptions.JsonKey))
            .Configure<PublisherAccountOptions>(
                configuration.GetSection(PublisherAccountOptions.JsonKey))
            .Configure<SchedulerOptions>(
                configuration.GetSection(SchedulerOptions.JsonKey))
            .Configure<SchedulerDashboardOptions>(
                configuration.GetSection(SchedulerDashboardOptions.JsonKey));

        return services;
    }
}
