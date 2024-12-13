//using System.Text;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Documents.Building;

//public class SalesReportDocumentBuilder : IDocumentBuilder<SalesReport>
//{
//    private readonly IDocumentBuilder<Sale> _contentBuilder;
//    private readonly IFormatter<SalesReport> _formatter;

//    public SalesReportDocumentBuilder(
//        IDocumentBuilder<Sale> contentBuilder,
//        IFormatter<SalesReport> formatter)
//    {
//        _contentBuilder = contentBuilder;
//        _formatter = formatter;
//    }

//    public string Build(SalesReport report, BuildSettings? settings = null)
//    {
//        if (settings.HasValue)
//        {
//            AdjustFormatting(settings.Value);
//        }

//        StringBuilder document = new();

//        AppendHeader(document);
//        AppendContent(report.Sales, document);
//        AppendFooter(report, document);

//        return document.ToString();
//    }

//    public void AdjustFormatting(BuildSettings settings)
//    {
//        _formatter.SetMargin(settings.Margin);
//    }

//    private void AppendHeader(StringBuilder document)
//    {
//        document.AppendLine(value: _formatter.FormatLine("SALES:\n"));
//    }

//    private void AppendContent(Sale[] sales, StringBuilder document)
//    {
//        string separator = _formatter.ContentSeparator;

//        _contentBuilder.AdjustFormatting(new BuildSettings
//        {
//            Margin = _formatter.Options.Padding + 1
//        });

//        for (int i = 0; i < sales.Length; ++i)
//        {
//            document
//                .AppendLine(value: _contentBuilder.Build(sales[i]))
//                .AppendLine(value: separator);
//        }
//    }

//    private void AppendFooter(SalesReport report, StringBuilder document)
//    {
//        document.AppendLine(
//            value: _formatter.FormatLines(
//                $"Total items sold: {report.ItemsSold}.",
//                $"Total revenue: {report.Revenue}."));
//    }
//}
