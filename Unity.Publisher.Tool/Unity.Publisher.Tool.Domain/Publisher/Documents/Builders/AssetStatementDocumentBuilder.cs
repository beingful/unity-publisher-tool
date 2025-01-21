using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class AssetStatementDocumentBuilder : IDocumentBuilder<AssetStatement>
{
    private readonly IDocumentBuilder<Sales> _salesContentBuilder;
    private readonly IDocumentBuilder<Reviews> _reviewsContentBuilder;
    private readonly IDocumentBuilder<Download> _downloadsContentBuilder;

    public AssetStatementDocumentBuilder(
        IDocumentBuilder<Sales> salesContentBuilder,
        IDocumentBuilder<Reviews> reviewsContentBuilder,
        IDocumentBuilder<Download> downloadsContentBuilder)
    {
        _salesContentBuilder = salesContentBuilder;
        _reviewsContentBuilder = reviewsContentBuilder;
        _downloadsContentBuilder = downloadsContentBuilder;
    }

    public IDocument Build(AssetStatement statement)
    {
        Metadata metadata = Metadata.WithDescription(statement.Asset.Name);

        Content content = new(
            text: GetContent(statement),
            formatting: new ParagraphFormatting());

        Document document = Document.Create(content, metadata);

        GetInnerDocuments(statement).ForEach(inner => document.AddInner(inner));

        return document;
    }

    private string GetContent(AssetStatement statement)
    {
        return $"{statement.Asset.Name.ToUpper()}\n";
    }

    private List<IDocument> GetInnerDocuments(AssetStatement statement)
    {
        List<IDocument> documents = new();

        if (statement.Sales.IsEmpty == false)
        {
            documents.Add(_salesContentBuilder.Build(statement.Sales));
        }

        if (statement.Reviews.IsEmpty == false)
        {
            documents.Add(_reviewsContentBuilder.Build(statement.Reviews));
        }

        if (statement.Downloads.IsEmpty == false)
        {
            documents.Add(_downloadsContentBuilder.Build(statement.Downloads));
        }

        return documents;
    }
}
