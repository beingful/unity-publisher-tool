using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Options;

namespace Unity.Publisher.Tool.Dependencies;

public static class ConfigurationOptionsInjection
{
    public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, WebApplicationBuilder builder)
    {
        IConfigurationSection optionsSection = builder.Configuration.GetSection("Options");

        return services
            .Configure<PublisherAccountOptions>(
                optionsSection.GetSection(PublisherAccountOptions.Path))
            .Configure<SchedulingOptions>(
                optionsSection.GetSection(SchedulingOptions.Path));
    }
}
