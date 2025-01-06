using Unity.Publisher.Tool.Domain.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class PublisherReportDocumentBuilder : IDocumentBuilder<PublisherReport>
{
    private readonly IDocumentParagraphBuilder<PublisherStatement> _contentBuilder;

    public PublisherReportDocumentBuilder(IDocumentParagraphBuilder<PublisherStatement> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(PublisherReport report)
    {
        Title title = new(name: "Report", description: "Report");

        Content content = new(
            text: Content(report),
            formatting: new DocumentFormatting(
                baseFormatting: new ParagraphFormatting()));

        return Document.Create(title, content)
            .AddInner(NestedDocument(report.Statement));
    }

    private string Content(PublisherReport report)
    {
        return "REPORT\n\n" +
            $"Publisher: {report.Publisher.Name}\n" +
            $"Publisher rating: {report.Publisher.Rating.Average}\n" +
            $"This month revenue: {report.Revenue.ForPeriod}\n" +
            $"Total revenue: {report.Revenue.Total}\n" +
            $"Month reported: {report.Month.Name}\n";
    }

    private IDocument NestedDocument(PublisherStatement statement)
    {
        return _contentBuilder.Build(statement);
    }
}
