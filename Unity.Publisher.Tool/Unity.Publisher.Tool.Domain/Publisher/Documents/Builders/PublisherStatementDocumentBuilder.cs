using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class PublisherStatementDocumentBuilder
    : IDocumentBuilder<PublisherStatement>, IParagraphBuilder<PublisherStatement>
{
    private readonly IParagraphBuilder<AssetStatement> _contentBuilder;

    public PublisherStatementDocumentBuilder(IParagraphBuilder<AssetStatement> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    IDocument IDocumentBuilder<PublisherStatement>.Build(PublisherStatement statement)
    {
        Title title = new(name: "StatementUpdate", description: "Updates");

        Content content = new(
            text: Content(),
            formatting: new DocumentFormatting(
                baseFormatting: new ParagraphFormatting(
                    options: new FormattingOptions { Separator = '-' })));

        Document document = Document.Create(title, content);

        InnerDocuments(statement.AssetsStatements)
            .ForEach(inner => document.AddInner(inner));

        return document;
    }

    IDocument IParagraphBuilder<PublisherStatement>.Build(PublisherStatement statement)
    {
        Content content = new(
            text: Content(),
            formatting: new ParagraphFormatting(
                options: new FormattingOptions { Separator = '-' }));

        Document document = Document.CreateParagraph(content);

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
