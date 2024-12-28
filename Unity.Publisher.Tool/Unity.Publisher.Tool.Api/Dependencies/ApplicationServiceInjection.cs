using Autofac;
using Hangfire;
using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.App.Services;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

namespace Unity.Publisher.Tool.Dependencies;

public static class ApplicationServiceInjection
{
    public static ContainerBuilder AddApplicationSrevices(this ContainerBuilder container)
    {
        container
            .RegisterTypes(
                typeof(StatementUpdateEventScheduler),
                typeof(MonthlyReportEventScheduler))
            .InstancePerLifetimeScope();

        container
            .Register<PublisherNotificationScheduler>(sp =>
            {
                return new PublisherNotificationScheduler(
                    schedulers: new KeyedProvider<PublisherEvent, IPublisherEventNotificationScheduler>(
                        components: new Dictionary<PublisherEvent, IPublisherEventNotificationScheduler>()
                        {
                            { PublisherEvent.StatementUpdate, sp.Resolve<StatementUpdateEventScheduler>() },
                            { PublisherEvent.MonthlyReport, sp.Resolve<MonthlyReportEventScheduler>() }
                        }));
            })
            .InstancePerLifetimeScope();

        container
            .RegisterTypes(
                typeof(PublisherStatementService),
                typeof(PublisherStatementHandler))
            .InstancePerBackgroundJob();

        container
            .RegisterType<StatementUpdateEventHandler>()
            .InstancePerBackgroundJob();

        container
            .Register<PublisherEventNotificationService<PublisherStatement>>(sp =>
            {
                return new PublisherEventNotificationService<PublisherStatement>(
                    documentExporter: sp.Resolve<DocumentExporter<PublisherStatement>>(),
                    idProvider: new PublisherEventIdProvider(PublisherEvent.StatementUpdate),
                    storage: sp.Resolve<IScheduleStorage>());
            })
            .As<IHandler<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .Register<PublisherEventNotificationService<PublisherReport>>(sp =>
            {
                return new PublisherEventNotificationService<PublisherReport>(
                    documentExporter: sp.Resolve<DocumentExporter<PublisherReport>>(),
                    idProvider: new PublisherEventIdProvider(PublisherEvent.MonthlyReport),
                    storage: sp.Resolve<IScheduleStorage>());
            })
            .As<IHandler<PublisherReport>>()
            .InstancePerBackgroundJob();

        container
            .RegisterGeneric(typeof(DocumentExporter<>))
            .InstancePerBackgroundJob();

        container
            .RegisterType<StatementUpdateEventService>()
            .As<IDataService<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<MonthlyReportEventService>()
            .As<IDataService<PublisherReport>>()
            .InstancePerBackgroundJob();

        return container;
    }
}
