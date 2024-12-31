using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Builders;

public class PublisherReportDocumentBuilder : IDocumentBuilder<PublisherReport>
{
    private readonly IDocumentBuilder<PublisherStatement> _contentBuilder;

    public PublisherReportDocumentBuilder(IDocumentBuilder<PublisherStatement> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(PublisherReport report)
    {
        Title title = new(name: "Report", description: "Report");

        Content content = new(
            text: Content(report),
            formatting: new DocumentFormatting());

        return Document.Create(title, content)
            .AddInner(NestedDocument(report.Statement));
    }

    private string Content(PublisherReport report)
    {
        return "REPORT\n\n" +
            $"Publisher: {report.Publisher.Name}" +
            $"Publisher rating: {report.Publisher.Rating.Average}" +
            $"This month revenue: {report.Revenue.ForPeriod}" +
            $"Total revenue: {report.Revenue.Total}" +
            $"Month reported: {report.Month.Name}\n";
    }

    private IDocument NestedDocument(PublisherStatement statement)
    {
        return _contentBuilder.Build(statement);
    }
}
