using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class AssetStatementDocumentBuilder : IParagraphBuilder<AssetStatement>
{
    private readonly IParagraphBuilder<Sales> _salesContentBuilder;
    private readonly IParagraphBuilder<Reviews> _reviewsContentBuilder;
    private readonly IParagraphBuilder<Download> _downloadsContentBuilder;

    public AssetStatementDocumentBuilder(
        IParagraphBuilder<Sales> salesContentBuilder,
        IParagraphBuilder<Reviews> reviewsContentBuilder,
        IParagraphBuilder<Download> downloadsContentBuilder)
    {
        _salesContentBuilder = salesContentBuilder;
        _reviewsContentBuilder = reviewsContentBuilder;
        _downloadsContentBuilder = downloadsContentBuilder;
    }

    public IDocument Build(AssetStatement statement)
    {
        Content content = new(
            text: Content(statement),
            formatting: new ParagraphFormatting());

        Document document = Document.CreateParagraph(content);

        InnerDocuments(statement).ForEach(inner => document.AddInner(inner));

        return document;
    }

    private string Content(AssetStatement statement)
    {
        return $"{statement.Asset.Name.ToUpper()}\n";
    }

    private List<IDocument> InnerDocuments(AssetStatement statement)
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
