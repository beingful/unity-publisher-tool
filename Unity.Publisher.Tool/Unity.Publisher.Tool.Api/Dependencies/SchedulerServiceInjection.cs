using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Unity.Publisher.Tool.Access;
using Unity.Publisher.Tool.Access.Options;
using Unity.Publisher.Tool.Infrastructure.Db.Redis;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Attributes;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Dashboard;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Options;

namespace Unity.Publisher.Tool.Dependencies;

public static class SchedulerServiceInjection
{
    public static IServiceCollection AddScheduler(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddHangfire((sp, globalConfiguration) =>
            {
                string connectionString = configuration.GetConnectionString(nameof(RedisDb))!;

                globalConfiguration
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
            .AddHangfireServer();
    }

    public static IApplicationBuilder UseDashboard(this IApplicationBuilder app, IConfiguration configuration)
    {
        SchedulerDashboardOptions dashboardOptions = app.ApplicationServices
            .GetRequiredService<IOptions<SchedulerDashboardOptions>>().Value;

        return app.UseHangfireDashboard(
            pathMatch: dashboardOptions.Path,
            options: new DashboardOptions
            {
                DashboardTitle = dashboardOptions.Title,
                Authorization = [ new DashboardAccessFilter() ],
                IsReadOnlyFunc = context =>
                {
                    AuthorizationOptions authorizationOptions = app.ApplicationServices
                        .GetRequiredService<IOptions<AuthorizationOptions>>().Value;

                    AdminAuthorizationRule adminRule = new(authorizationOptions.Admin);

                    HttpContext httpContext = context.GetHttpContext();

                    return adminRule.Authorize(httpContext.User) == false;
                }
            });
    }
}
