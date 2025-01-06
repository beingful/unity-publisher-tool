using Unity.Publisher.Tool.Domain.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class AssetStatementDocumentBuilder : IDocumentParagraphBuilder<AssetStatement>
{
    private readonly IDocumentParagraphBuilder<Sales> _salesContentBuilder;
    private readonly IDocumentParagraphBuilder<Reviews> _reviewsContentBuilder;
    private readonly IDocumentParagraphBuilder<Download> _downloadsContentBuilder;

    public AssetStatementDocumentBuilder(
        IDocumentParagraphBuilder<Sales> salesContentBuilder,
        IDocumentParagraphBuilder<Reviews> reviewsContentBuilder,
        IDocumentParagraphBuilder<Download> downloadsContentBuilder)
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
