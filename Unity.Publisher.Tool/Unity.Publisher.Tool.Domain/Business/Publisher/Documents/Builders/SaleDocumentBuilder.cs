using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

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
