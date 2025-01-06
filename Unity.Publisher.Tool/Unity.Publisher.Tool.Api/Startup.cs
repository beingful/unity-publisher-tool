using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.Redis.StackExchange;
using StackExchange.Redis;
using Unity.Publisher.Tool.Dependencies;
using Unity.Publisher.Tool.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Db.Redis;

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
                            UseTransactions = false
                        })
                    .UseFilter(new AutomaticRetryAttribute
                    {
                        Attempts = 0,
                        OnAttemptsExceeded = AttemptsExceededAction.Delete
                    });
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
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHangfireDashboard();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(x =>
        {
            x.AddNotificationEndpoints();
        });

        GlobalConfiguration.Configuration
            .UseAutofacActivator(app.ApplicationServices.GetAutofacRoot());
    }
}
