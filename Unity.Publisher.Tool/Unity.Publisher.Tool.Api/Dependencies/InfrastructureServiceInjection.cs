using Autofac;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Unity.Publisher.Tool.Domain.Data.Providers;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Options;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Session;
using Unity.Publisher.Tool.Infrastructure.Api.State;
using Unity.Publisher.Tool.Infrastructure.Db;
using Unity.Publisher.Tool.Infrastructure.Db.Redis;
using Unity.Publisher.Tool.Infrastructure.Db.Redis.Repositories;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Notification;
using Unity.Publisher.Tool.Infrastructure.Notification.Emails;
using Unity.Publisher.Tool.Infrastructure.Notification.Emails.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;
using IHttpClientFactory = Unity.Publisher.Tool.Infrastructure.Http.Clients.IHttpClientFactory;

namespace Unity.Publisher.Tool.Dependencies;

public static class InfrastructureServiceInjection
{
    public static ContainerBuilder AddInfrastructureServices(this ContainerBuilder container)
    {
        container
            .RegisterType<SmtpServersCollection>()
            .SingleInstance();

        container
            .RegisterType<EmailNotificator>()
            .As<INotificator<EmailNotification>>();

        container
            .RegisterType<HttpClientFactory>()
            .As<IHttpClientFactory>()
            .SingleInstance();

        container
            .RegisterGeneric(typeof(DynamicHttpClient<>))
            .As(typeof(IHttpClient<>))
            .InstancePerLifetimeScope();

        container
            .Register<KeyedProvider<ISessionManager>>((context) =>
            {
                return new KeyedProvider<ISessionManager>(
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
            .Register<KeyedProvider<ILogInManager>>((context) =>
            {
                return new KeyedProvider<ILogInManager>(
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
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        container
            .Register<ConnectionMultiplexer>((context) =>
            {
                IConfiguration configuration = context.Resolve<IConfiguration>();

                string connectionString = configuration.GetConnectionString(nameof(RedisDb))!;

                return ConnectionMultiplexer.Connect(connectionString);
            })
            .InstancePerLifetimeScope();

        container
            .RegisterType<RedisDb>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<RedisDbRepository>()
            .As<IStorage>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<RedisDbRepository>()
            .As<IStorage>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<Scheduler>()
            .As<IScheduler>();

        return container;
    }
}
