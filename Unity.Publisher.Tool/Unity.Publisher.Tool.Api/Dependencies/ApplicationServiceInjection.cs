using Autofac;
using Hangfire;
using Unity.Publisher.Tool.App.Services;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Infrastructure.Scheduling;

namespace Unity.Publisher.Tool.Dependencies;

public static class ApplicationServiceInjection
{
    public static ContainerBuilder AddApplicationSrevices(this ContainerBuilder container)
    {
        return container
            .AddPublisherEventNotificationSrevice()
            .AddPublisherEventSubscriptionSrevices(PublisherEvent.StatementUpdate)
            .AddPublisherEventSubscriptionSrevices(PublisherEvent.MonthlyReport);
    }

    private static ContainerBuilder AddPublisherEventNotificationSrevice(this ContainerBuilder container)
    {
        container
            .Register<PublisherEventNotificationService>(sp =>
            {
                return new PublisherEventNotificationService(
                    new KeyedProvider<PublisherEvent, IPublisherEventNotificationSubscriber>(
                        new Dictionary<PublisherEvent, IPublisherEventNotificationSubscriber>()
                        {
                            {
                                PublisherEvent.StatementUpdate,
                                sp.ResolveKeyed<IPublisherEventNotificationSubscriber>(PublisherEvent.StatementUpdate)
                            },
                            {
                                PublisherEvent.MonthlyReport,
                                sp.ResolveKeyed<IPublisherEventNotificationSubscriber>(PublisherEvent.MonthlyReport)
                            }
                        }));
            })
            .InstancePerLifetimeScope();

        return container;
    }

    private static ContainerBuilder AddPublisherEventSubscriptionSrevices(this ContainerBuilder container, PublisherEvent publisherEvent)
    {
        return publisherEvent switch
        {
            PublisherEvent.StatementUpdate => container.AddStatementUpdateSubscriptionSrevices(),
            PublisherEvent.MonthlyReport => container.AddMonthlyReportSubscriptionSrevices(),
            _ => throw new ArgumentException($"The \'{publisherEvent}\' event is not supported.")
        };
    }

    public static ContainerBuilder AddStatementUpdateSubscriptionSrevices(this ContainerBuilder container)
    {
        PublisherEvent key = PublisherEvent.StatementUpdate;

        container.Register<StatementUpdateSubscriber>(sp =>
        {
            return new StatementUpdateSubscriber(
                publisherEventIdProvider: sp.ResolveKeyed<IPublisherEventIdProvider>(key),
                scheduler: sp.Resolve<IScheduler>());
        })
        .Keyed<IPublisherEventNotificationSubscriber>(key)
        .InstancePerLifetimeScope();

        container
            .RegisterType<StatementUpdatePerformer>()
            .InstancePerBackgroundJob();

        return container;
    }

    public static ContainerBuilder AddMonthlyReportSubscriptionSrevices(this ContainerBuilder container)
    {
        PublisherEvent key = PublisherEvent.MonthlyReport;

        container.Register<MonthlyReportSubscriber>(sp =>
        {
            return new MonthlyReportSubscriber(
                publisherEventIdProvider: sp.ResolveKeyed<IPublisherEventIdProvider>(key),
                scheduler: sp.Resolve<IScheduler>());
        })
        .Keyed<IPublisherEventNotificationSubscriber>(key)
        .InstancePerLifetimeScope();

        container
            .RegisterType<MonthlyReportPerformer>()
            .InstancePerBackgroundJob();

        return container;
    }
}
