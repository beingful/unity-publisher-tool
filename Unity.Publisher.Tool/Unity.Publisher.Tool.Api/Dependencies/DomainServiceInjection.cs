using Autofac;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
using Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Comparers;
using Hangfire;

namespace Unity.Publisher.Tool.Dependencies;

public static class DomainServiceInjection
{
    public static ContainerBuilder AddDomainServices(this ContainerBuilder container)
    {
        container
            .Register<Month>(context =>
            {
                return new Month(order: DateTime.UtcNow.Month);
            })
            .InstancePerLifetimeScope();

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

        container
            .RegisterType<PublisherStatementDocumentBuilder>()
            .As<IDocumentBuilder<PublisherStatement>>()
            .InstancePerLifetimeScope()
            .InstancePerBackgroundJob();

        container
            .RegisterType<AssetStatementDocumentBuilder>()
            .As<IDocumentBuilder<AssetStatement>>()
            .InstancePerLifetimeScope()
            .InstancePerBackgroundJob();

        container
            .RegisterType<SalesDocumentBuilder>()
            .As<IDocumentBuilder<Sales>>()
            .InstancePerLifetimeScope()
            .InstancePerBackgroundJob();

        container
            .RegisterType<SaleDocumentBuilder>()
            .As<IDocumentBuilder<Sale>>()
            .InstancePerLifetimeScope()
            .InstancePerBackgroundJob();

        container
            .RegisterType<ReviewsDocumentBuilder>()
            .As<IDocumentBuilder<Reviews>>()
            .InstancePerLifetimeScope()
            .InstancePerBackgroundJob();

        container
            .RegisterType<ReviewDocumentBuilder>()
            .As<IDocumentBuilder<Review>>()
            .InstancePerLifetimeScope()
            .InstancePerBackgroundJob();

        container
            .RegisterType<DownloadDocumentBuilder>()
            .As<IDocumentBuilder<Download>>()
            .InstancePerLifetimeScope()
            .InstancePerBackgroundJob();

        return container;
    }
}
