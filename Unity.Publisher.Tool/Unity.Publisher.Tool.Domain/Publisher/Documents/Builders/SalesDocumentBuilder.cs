using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class SalesDocumentBuilder : IParagraphBuilder<Sales>
{
    private readonly IParagraphBuilder<Sale> _contentBuilder;

    public SalesDocumentBuilder(IParagraphBuilder<Sale> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(Sales sales)
    {
        Content content = new(
            text: Content(sales),
            formatting: new ParagraphFormatting());

        Document document = Document.CreateParagraph(content);

        InnerDocuments(sales).ForEach(inner => document.AddInner(inner));

        return document;
    }

    private string Content(Sales sales)
    {
        return $"NEW SALES:\n\nRevenue: {sales.Revenue}.\n";
    }

    private List<IDocument> InnerDocuments(Sales sales)
    {
        List<IDocument> documents = new(sales.Count);

        foreach (Sale sale in sales.Collection)
        {
            documents.Add(_contentBuilder.Build(sale));
        }

        return documents;
    }
}
