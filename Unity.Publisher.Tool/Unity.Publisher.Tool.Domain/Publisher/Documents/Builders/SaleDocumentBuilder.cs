using Unity.Publisher.Tool.Domain.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class SaleDocumentBuilder : IDocumentParagraphBuilder<Sale>
{
    public IDocument Build(Sale sale)
    {
        Content content = new(
            text: Content(sale),
            formatting: new ParagraphFormatting());

        return Document.CreateParagraph(content);
    }

    private string Content(Sale sale)
    {
        return $"{sale.CopiesSold} copies sold " +
            $"at ${sale.ProductTag.Price} each.";
    }
}
