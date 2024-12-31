using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Builders;

public class SaleDocumentBuilder : IDocumentBuilder<Sale>
{
    public IDocument Build(Sale sale)
    {
        Content content = new(
            text: Content(sale),
            formatting: new DocumentFormatting());

        return Document.CreateParagraph(content);
    }

    private string Content(Sale sale)
    {
        return $"{sale.CopiesSold} copies are sold" +
            $"at ${sale.ProductTag.Price} each.";
    }
}
