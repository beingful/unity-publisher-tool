using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Builders;

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
        Content content = new(
            text: Content(statement),
            formatting: new DocumentFormatting());

        IDocument document = Document.CreateParagraph(content);

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
