using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class PublisherStatementDocumentBuilder : IDocumentBuilder<PublisherStatement>
{
    private readonly IDocumentBuilder<AssetStatement> _contentBuilder;

    public PublisherStatementDocumentBuilder(IDocumentBuilder<AssetStatement> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(PublisherStatement statement)
    {
        Title title = new(name: "StatementUpdate", description: "Updates");

        Content content = new(
            text: Content(),
            formatting: new DocumentFormatting(
                options: new FormattingOptions { Separator = '-' }));

        IDocument document = Document.Create(title, content);

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
