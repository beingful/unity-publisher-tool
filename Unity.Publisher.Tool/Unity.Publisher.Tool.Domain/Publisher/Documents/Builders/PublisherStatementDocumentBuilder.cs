using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class PublisherStatementDocumentBuilder: IDocumentBuilder<PublisherStatement>
{
    private readonly IDocumentBuilder<AssetStatement> _contentBuilder;

    public PublisherStatementDocumentBuilder(IDocumentBuilder<AssetStatement> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(PublisherStatement statement)
    {
        Metadata metadata = Metadata.WithTitle("StatementUpdate");

        Content content = new(
            text: Content(),
            formatting: new DocumentFormatting(
                baseFormatting: new ParagraphFormatting(
                    options: new FormattingOptions { Separator = '*' })));

        Document document = Document.Create(content, metadata);

        InnerDocuments(statement.AssetsStatements)
            .ForEach(inner => document.AddInner(inner));

        return document;
    }

    internal string Content()
    {
        return "LATEST UPDATES:\n";
    }

    private List<IDocument> InnerDocuments(AssetStatement[] assetStatements)
    {
        List<IDocument> documents = new(assetStatements.Length);

        foreach (AssetStatement statement in assetStatements)
        {
            documents.Add(_contentBuilder.Build(statement));
        }

        return documents;
    }
}
