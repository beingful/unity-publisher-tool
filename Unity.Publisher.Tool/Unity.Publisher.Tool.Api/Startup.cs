using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.Redis.StackExchange;
using StackExchange.Redis;
using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Dependencies;
using Unity.Publisher.Tool.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Db.Redis;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Attributes;

namespace Unity.Publisher.Tool;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddHangfire((sp, configuration) =>
            {
                string connectionString = Configuration.GetConnectionString(nameof(RedisDb))!;

                configuration
                    .UseRedisStorage(
                        ConnectionMultiplexer.Connect(connectionString),
                        new RedisStorageOptions
                        {
                            UseTransactions = false,
                            DeletedListSize = 20,
                            InvisibilityTimeout = TimeSpan.FromSeconds(30)
                        })
                    .UseFilter(new AutomaticRetryAttribute
                    {
                        Attempts = 0,
                        OnAttemptsExceeded = AttemptsExceededAction.Delete
                    })
                    .UseFilter(new DeleteOnSuccessAttribute(seconds: 30));
            })
            .AddHangfireServer()
            .AddConfigurationOptions(Configuration);
    }

    public void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder
            .AddApplicationSrevices()
            .AddInfrastructureServices()
            .AddDomainServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHangfireDashboard();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .AddNotificationEndpoints()
                .AddHomeEndpoints();
        });

        GlobalConfiguration.Configuration
            .UseAutofacActivator(app.ApplicationServices.GetAutofacRoot());
    }
}
