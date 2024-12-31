using Autofac;
using Hangfire;
using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.App.Services;
using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

namespace Unity.Publisher.Tool.Dependencies;

public static class ApplicationServiceInjection
{
    public static ContainerBuilder AddApplicationSrevices(this ContainerBuilder container)
    {
        container
            .Register<PublisherNotificationScheduler>(sp =>
            {
                return new PublisherNotificationScheduler(
                    schedulers: new KeyedProvider<PublisherEvent, INotificationScheduler>(
                        components: new Dictionary<PublisherEvent, INotificationScheduler>()
                        {
                            {
                                PublisherEvent.StatementUpdate,
                                sp.ResolveKeyed<INotificationScheduler>(PublisherEvent.StatementUpdate)
                            },
                            {
                                PublisherEvent.MonthlyReport,
                                sp.ResolveKeyed<INotificationScheduler>(PublisherEvent.MonthlyReport) }
                        }));
            })
            .InstancePerLifetimeScope();

        container
            .RegisterType<TypeNameProvider>()
            .As<IPublisherEventDataNameProvider<Type>>()
            .InstancePerBackgroundJob();

        container
            .AddPublisherEventSpecificSrevices(PublisherEvent.StatementUpdate)
            .AddPublisherEventSpecificSrevices(PublisherEvent.MonthlyReport);

        return container;
    }

    public static ContainerBuilder AddPublisherEventSpecificSrevices(this ContainerBuilder container, PublisherEvent publisherEvent)
    {
        return publisherEvent switch
        {
            PublisherEvent.StatementUpdate => container.AddStatementUpdateSrevices(),
            PublisherEvent.MonthlyReport => container.AddMonthlyReportSrevices()
        };
    }

    public static ContainerBuilder AddStatementUpdateSrevices(this ContainerBuilder container)
    {
        PublisherEvent key = PublisherEvent.StatementUpdate;

        container
            .RegisterType<PublisherStatementService>()
            .InstancePerBackgroundJob();

        container.Register<PublisherEventIdProvider>(sp =>
        {
            return new PublisherEventIdProvider(key);
        })
        .Keyed<IPublisherEventIdProvider>(key)
        .InstancePerLifetimeScope();

        container.Register<StatementUpdateEventScheduler>(sp =>
        {
            return new StatementUpdateEventScheduler(
                publisherEventIdProvider: sp.ResolveKeyed<IPublisherEventIdProvider>(key),
                scheduler: sp.Resolve<IScheduler>());
        })
        .Keyed<INotificationScheduler>(key)
        .InstancePerLifetimeScope();

        container.Register<PublisherEventDataStorage>(sp =>
        {
             return new(
                 publisherEventIdProvider: sp.ResolveKeyed<IPublisherEventIdProvider>(key),
                 publisherEventDataNameProvider: sp.ResolveKeyed<IPublisherEventDataNameProvider<Type>>(key),
                 storage: sp.Resolve<IJobDataStorage>());
        })
        .As<IPublisherEventDataStorage>()
        .InstancePerBackgroundJob();

        container.Register<PublisherEventHandler<PublisherStatement>>(sp =>
        {
            return new(
                dataService: sp.Resolve<IDataService<PublisherStatement>>(),
                eventOccured: statement => statement.IsEmpty == false,
                documentExporter: sp.Resolve<DocumentExporter<PublisherStatement>>());
        })
        .InstancePerBackgroundJob();

        container
            .RegisterType<DocumentExporter<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<StatementUpdateEventService>()
            .As<IDataService<PublisherStatement>>()
            .InstancePerBackgroundJob();

        return container;
    }

    public static ContainerBuilder AddMonthlyReportSrevices(this ContainerBuilder container)
    {
        PublisherEvent key = PublisherEvent.MonthlyReport;

        container.Register<PublisherEventIdProvider>(sp =>
        {
            return new PublisherEventIdProvider(key);
        })
        .Keyed<IPublisherEventIdProvider>(key)
        .InstancePerLifetimeScope();

        container.Register<MonthlyReportEventScheduler>(sp =>
        {
            return new MonthlyReportEventScheduler(
                publisherEventIdProvider: sp.ResolveKeyed<IPublisherEventIdProvider>(key),
                scheduler: sp.Resolve<IScheduler>());
        })
        .Keyed<INotificationScheduler>(key)
        .InstancePerLifetimeScope();

        container.Register<PublisherEventHandler<PublisherReport>>(sp =>
        {
            return new(
                dataService: sp.Resolve<IDataService<PublisherReport>>(),
                eventOccured: _ => true,
                documentExporter: sp.Resolve<DocumentExporter<PublisherReport>>());
        })
        .InstancePerBackgroundJob();

        container
            .RegisterType<DocumentExporter<PublisherReport>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<MonthlyReportEventService>()
            .As<IDataService<PublisherReport>>()
            .InstancePerBackgroundJob();

        return container;
    }
}
