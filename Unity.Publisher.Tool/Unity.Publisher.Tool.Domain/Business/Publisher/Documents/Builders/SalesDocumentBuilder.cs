using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class SalesDocumentBuilder : IDocumentBuilder<Sales>
{
    private readonly IDocumentBuilder<Sale> _contentBuilder;

    public SalesDocumentBuilder(IDocumentBuilder<Sale> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(Sales sales)
    {
        Content content = new(
            text: Content(sales),
            formatting: new DocumentFormatting());

        IDocument document = Document.CreateParagraph(content);

        InnerDocuments(sales).ForEach(inner => document.AddInner(inner));

        return document;
    }

    private string Content(Sales sales)
    {
        return $"NEW SALES:\n\nRevenue: {sales.Revenue}\n";
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
