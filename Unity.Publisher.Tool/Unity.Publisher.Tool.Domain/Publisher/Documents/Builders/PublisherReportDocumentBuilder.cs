using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class PublisherReportDocumentBuilder : IDocumentBuilder<PublisherReport>
{
    private readonly IDocumentBuilder<PublisherInfo> _publisherInfoContentBuilder;
    private readonly IDocumentBuilder<PublisherStatement> _publisherStatementContentBuilder;

    public PublisherReportDocumentBuilder(
        IDocumentBuilder<PublisherInfo> publisherInfoContentBuilder,
        IDocumentBuilder<PublisherStatement> publisherStatementContentBuilder)
    {
        _publisherInfoContentBuilder = publisherInfoContentBuilder;
        _publisherStatementContentBuilder = publisherStatementContentBuilder;
    }

    public IDocument Build(PublisherReport report)
    {
        Metadata metadata = Metadata.Create(title: "Report", description: "Report");

        Content content = new(
            text: Content(report),
            formatting: new DocumentFormatting(
                baseFormatting: new ParagraphFormatting()));

        SelfDescriptiveDocument document = new(content, metadata);

        InnerDocuments(report).ForEach(inner => document.AddInner(inner));

        return document;
    }

    private string Content(PublisherReport report)
    {
        return $"REPORT FOR {report.Month.Name}:\n\n" +
            $"Total revenue: ${report.Revenue.Total}.\n";
    }

    private List<IDocument> InnerDocuments(PublisherReport report)
    {
        return new List<IDocument>()
        {
            _publisherInfoContentBuilder.Build(report.Publisher),
            _publisherStatementContentBuilder.Build(report.Statement)
        };
    }
}
