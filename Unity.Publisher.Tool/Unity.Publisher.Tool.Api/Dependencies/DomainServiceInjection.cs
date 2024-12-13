using Autofac;
using Unity.Publisher.Tool.Domain.Data.Providers;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Comparers;

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
            .As<IDataComparer<PublisherStatement>>();

        container
            .RegisterType<AssetStatementComparer>()
            .As<IDataComparer<AssetStatement>>();

        container
            .RegisterType<SalesComparer>()
            .As<IDataComparer<Sales>>();

        container
            .RegisterType<SaleComparer>()
            .As<IDataComparer<Sale>>();

        container
            .RegisterType<ReviewsComparer>()
            .As<IDataComparer<Reviews>>();

        container
            .RegisterType<DownloadComparer>()
            .As<IDataComparer<Download>>();

        container
            .RegisterType<FormatterProvider>()
            .As<IKeyedProvider<Type, IFormatter>>()
            .SingleInstance();

        container
            .RegisterGeneric(typeof(DynamicFormatter<>))
            .As(typeof(IFormatter<>))
            .InstancePerLifetimeScope();

        container
            .RegisterType<PublisherStatementDocumentBuilder>()
            .As<IDocumentBuilder<PublisherStatement>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<AssetStatementDocumentBuilder>()
            .As<IDocumentBuilder<AssetStatement>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<SalesDocumentBuilder>()
            .As<IDocumentBuilder<Sales>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<SaleDocumentBuilder>()
            .As<IDocumentBuilder<Sale>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<ReviewsDocumentBuilder>()
            .As<IDocumentBuilder<Reviews>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<ReviewDocumentBuilder>()
            .As<IDocumentBuilder<Review>>()
            .InstancePerLifetimeScope();

        container
            .RegisterType<DownloadDocumentBuilder>()
            .As<IDocumentBuilder<Download>>()
            .InstancePerLifetimeScope();

        return container;
    }
}
