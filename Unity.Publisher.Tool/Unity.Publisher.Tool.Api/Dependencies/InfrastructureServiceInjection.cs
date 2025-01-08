using Autofac;
using Hangfire;
using Microsoft.Extensions.Options;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Notifications;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Storage;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Options;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Session;
using Unity.Publisher.Tool.Infrastructure.Api.State;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Notification.Emails;
using Unity.Publisher.Tool.Infrastructure.Notification.Emails.Services;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;
using IHttpClientFactory = Unity.Publisher.Tool.Infrastructure.Http.Clients.IHttpClientFactory;

namespace Unity.Publisher.Tool.Dependencies;

public static class InfrastructureServiceInjection
{
    public static ContainerBuilder AddInfrastructureServices(this ContainerBuilder container)
    {
        return container
            .AddNotificationServices()
            .AddHttpClient()
            .AddApiServices()
            .AddScheduleServices();
    }

    private static ContainerBuilder AddNotificationServices(this ContainerBuilder container)
    {
        container
            .RegisterType<SmtpServersCollection>()
            .SingleInstance();

        container
            .RegisterType<EmailNotificator>()
            .As<INotificator<EmailNotification>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<SimpleEmailNotificator>()
            .As<INotificator>()
            .InstancePerBackgroundJob();

        return container;
    }

    private static ContainerBuilder AddHttpClient(this ContainerBuilder container)
    {
        container
            .RegisterType<HttpClientFactory>()
            .As<IHttpClientFactory>()
            .SingleInstance();

        container
            .RegisterGeneric(typeof(DynamicHttpClient<>))
            .As(typeof(IHttpClient<>))
            .InstancePerLifetimeScope();

        return container;
    }

    private static ContainerBuilder AddApiServices(this ContainerBuilder container)
    {
        container
            .Register<TypeBasedProvider<ISessionManager>>(context =>
            {
                return new TypeBasedProvider<ISessionManager>(
                    components: new Dictionary<Type, ISessionManager>
                    {
                        {
                            typeof(PublisherApi), new PublisherSessionManager(
                                httpClient: context.Resolve<IHttpClient<PublisherApi>>(),
                                logInManager: context.Resolve<ILogInManager<PublisherApi>>(),
                                logger: context.Resolve<ILogger<PublisherSessionManager>>())
                        }
                    });
            })
            .As<IKeyedProvider<Type, ISessionManager>>()
            .InstancePerLifetimeScope();

        container
            .RegisterGeneric(typeof(DynamicSessionManager<>))
            .As(typeof(ISessionManager<>))
            .InstancePerLifetimeScope();

        container
            .Register<TypeBasedProvider<ILogInManager>>(context =>
            {
                return new TypeBasedProvider<ILogInManager>(
                    components: new Dictionary<Type, ILogInManager>
                    {
                        {
                            typeof(PublisherApi), new PublisherLogInManager(
                                httpClient: context.Resolve<IHttpClient<PublisherLogInManager>>(),
                                accountOptions: context.Resolve<IOptions<PublisherAccountOptions>>())
                        }
                    });
            })
            .As<IKeyedProvider<Type, ILogInManager>>()
            .InstancePerLifetimeScope();

        container
            .RegisterGeneric(typeof(DynamicLogInManager<>))
            .As(typeof(ILogInManager<>))
            .InstancePerLifetimeScope();

        container
            .RegisterType<PublisherApi>()
            .As<IDataSource<PublisherInfo>>()
            .As<IDataSource<Revenue>>()
            .As<IDataSource<Assets>>()
            .As<IDataSource<Sales>>()
            .As<IDataSource<Reviews>>()
            .As<IDataSource<Downloads>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherApi>()
            .As<IStartable>()
            .InstancePerLifetimeScope();

        return container;
    }

    private static ContainerBuilder AddScheduleServices(this ContainerBuilder container)
    {
        container
            .RegisterType<JobDataStorage>()
            .As<IKeyedDataStorage>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<Scheduler>()
            .As<IScheduler>()
            .InstancePerLifetimeScope();

        return container;
    }
}
