using Autofac;
using Hangfire;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Publisher.Comparers;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;
using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Dependencies;

public static class DomainServiceInjection
{
    public static ContainerBuilder AddDomainServices(this ContainerBuilder container)
    {
        return container
            .AddTimeCapturingServices()
            .AddDataComparisonServices()
            .AddDocumentServices()
            .AddDataStorageSrevices()
            .AddPublisherEventHandlingSrevices(PublisherEvent.StatementUpdate)
            .AddPublisherEventHandlingSrevices(PublisherEvent.MonthlyReport);
    }

    private static ContainerBuilder AddTimeCapturingServices(this ContainerBuilder container)
    {
        container
            .Register<DateTime>(context =>
            {
                return DateTime.UtcNow;
            })
            .InstancePerLifetimeScope();

        return container;
    }

    private static ContainerBuilder AddDataComparisonServices(this ContainerBuilder container)
    {
        container
            .RegisterType<PublisherStatementComparer>()
            .As<IDataComparer<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<AssetStatementComparer>()
            .As<IDataComparer<AssetStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<SalesComparer>()
            .As<IDataComparer<Sales>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<SaleComparer>()
            .As<IDataComparer<Sale>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<ReviewsComparer>()
            .As<IDataComparer<Reviews>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<DownloadComparer>()
            .As<IDataComparer<Download>>()
            .InstancePerBackgroundJob();

        return container;
    }

    private static ContainerBuilder AddDocumentServices(this ContainerBuilder container)
    {
        container
            .RegisterType<PublisherReportDocumentBuilder>()
            .As<IDocumentBuilder<PublisherReport>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherStatementDocumentBuilder>()
            .As<IDocumentBuilder<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherStatementDocumentBuilder>()
            .As<IDocumentBuilder<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherInfoDocumentBuilder>()
            .As<IDocumentBuilder<PublisherInfo>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<AssetStatementDocumentBuilder>()
            .As<IDocumentBuilder<AssetStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<SalesDocumentBuilder>()
            .As<IDocumentBuilder<Sales>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<SaleDocumentBuilder>()
            .As<IDocumentBuilder<Sale>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<ReviewsDocumentBuilder>()
            .As<IDocumentBuilder<Reviews>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<ReviewDocumentBuilder>()
            .As<IDocumentBuilder<Review>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<DownloadDocumentBuilder>()
            .As<IDocumentBuilder<Download>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherDocumentExporter<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherDocumentExporter<PublisherReport>>()
            .InstancePerBackgroundJob();

        return container;
    }

    private static ContainerBuilder AddDataStorageSrevices(this ContainerBuilder container)
    {
        container
            .Register<EnumBasedStringProvider<PublisherEvent>>(sp =>
            {
                return new EnumBasedStringProvider<PublisherEvent>(PublisherEvent.StatementUpdate);
            })
            .Keyed<IStorageKeyProvider>(PublisherEvent.StatementUpdate)
            .InstancePerBackgroundJob();

        container
            .RegisterType<TypeBasedStringProvider>()
            .As<IDataNameProvider>()
            .InstancePerBackgroundJob();

        container
            .Register<DataStorage>(sp =>
            {
                return new(
                    storageKeyProvider: sp.ResolveKeyed<IStorageKeyProvider>(PublisherEvent.StatementUpdate),
                    dataNameProvider: sp.Resolve<IDataNameProvider>(),
                    dataStorage: sp.Resolve<IKeyedDataStorage>());
            })
            .As<IDataStorage>()
            .InstancePerBackgroundJob();

        return container;
    }

    private static ContainerBuilder AddPublisherEventHandlingSrevices(this ContainerBuilder container, PublisherEvent publisherEvent)
    {
        return publisherEvent switch
        {
            PublisherEvent.StatementUpdate => container.AddStatementUpdateHandlingSrevices(),
            PublisherEvent.MonthlyReport => container.AddMonthlyReportHandlingSrevices(),
            _ => throw new ArgumentException($"The \'{publisherEvent}\' event is not supported.")
        };
    }

    private static ContainerBuilder AddStatementUpdateHandlingSrevices(this ContainerBuilder container)
    {
        PublisherEvent key = PublisherEvent.StatementUpdate;

        container
            .RegisterType<PublisherDownloadlessStatementService>()
            .As<IPublisherRefreshedStatementService>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherStoredStatementService>()
            .As<IPublisherStoredStatementService>()
            .InstancePerBackgroundJob();

        container
            .Register<EnumBasedStringProvider<PublisherEvent>>(sp =>
            {
                return new EnumBasedStringProvider<PublisherEvent>(key);
            })
            .Keyed<IPublisherEventIdProvider>(key)
            .InstancePerLifetimeScope();

        container
            .RegisterType<StatementUpdateService>()
            .As<IDataSource<PublisherStatement>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherStatementReportingService>()
            .InstancePerBackgroundJob();

        return container;
    }

    private static ContainerBuilder AddMonthlyReportHandlingSrevices(this ContainerBuilder container)
    {
        PublisherEvent key = PublisherEvent.MonthlyReport;

        container
            .Register<EnumBasedStringProvider<PublisherEvent>>(sp =>
            {
                return new EnumBasedStringProvider<PublisherEvent>(key);
            })
            .Keyed<IPublisherEventIdProvider>(key)
            .InstancePerLifetimeScope();

        container
            .RegisterType<PublisherFullStatementService>()
            .As<IPublisherStatementService>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<MonthlyReportService>()
            .As<IDataSource<PublisherReport>>()
            .InstancePerBackgroundJob();

        container
            .RegisterType<PublisherReportReportingService>()
            .InstancePerBackgroundJob();

        return container;
    }
}
