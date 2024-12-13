//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Documents.Building;

//public class SaleDocumentBuilder : IDocumentBuilder<Sale>
//{
//    private readonly IFormatter<Sale> _formatter;

//    public SaleDocumentBuilder(IFormatter<Sale> formatter)
//    {
//        _formatter = formatter;
//    }

//    public string Build(Sale sale, BuildSettings? settings = null)
//    {
//        if (settings.HasValue)
//        {
//            AdjustFormatting(settings.Value);
//        }

//        return AddContent(sale);
//    }

//    public void AdjustFormatting(BuildSettings settings)
//    {
//        _formatter.SetMargin(settings.Margin);
//    }

//    private string AddContent(Sale sale)
//    {
//        return _formatter.FormatLine($"{sale.CopiesSold} copies are sold at ${sale.Price} each.");
//    }
//}
