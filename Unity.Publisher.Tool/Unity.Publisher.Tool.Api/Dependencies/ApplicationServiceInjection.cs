using Autofac;
using Unity.Publisher.Tool.App.Services;
using Unity.Publisher.Tool.App.Services.Report;
using Unity.Publisher.Tool.App.Services.Statement;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using static Unity.Publisher.Tool.App.Models.PublisherEvents;

namespace Unity.Publisher.Tool.Dependencies;

public static class ApplicationServiceInjection
{
    public static ContainerBuilder AddApplicationSrevices(this ContainerBuilder container)
    {
        container
            .RegisterTypes(
                typeof(PublisherNotificationScheduler),
                typeof(PublisherStatementProvider),
                typeof(PublisherStatementHandler),
                typeof(EventScheduler<MonthlyReport, MonthlyReportEventInitiator>),
                typeof(EventScheduler<StatementUpdate, StatementUpdateEventInitiator>),
                typeof(StatementUpdateEventInitiator),
                typeof(MonthlyReportEventInitiator))
            .InstancePerLifetimeScope();

        container
            .RegisterType<StatementUpdateProvider>()
            .As<IEventDataProvider<PublisherStatement>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<MonthlyReportProvider>()
            .As<IEventDataProvider<PublisherReport>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<StatementUpdateNotificationScheduler>()
            .As<IEventNotificationScheduler<StatementUpdate>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<MonthlyReportNotificationScheduler>()
            .As<IEventNotificationScheduler<MonthlyReport>>()
            .InstancePerLifetimeScope();

        container
            .RegisterGeneric(typeof(EventNotificationHandler<>))
            .As(typeof(IEventHandler<>))
            .InstancePerLifetimeScope();

        return container;
    }
}
