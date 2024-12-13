//using System.Text;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Documents.Building;

//public class AssetReportDocumentBuilder : IDocumentBuilder<AssetReport>
//{
//    private readonly IDocumentBuilder<SalesReport> _salesContentBuilder;
//    private readonly IDocumentBuilder<ReviewsReport> _reviewsContentBuilder;
//    private readonly IDocumentBuilder<DownloadsReport> _downloadsContentBuilder;
//    private readonly IFormatter<AssetReport> _formatter;

//    public AssetReportDocumentBuilder(
//        IDocumentBuilder<SalesReport> salesContentBuilder,
//        IDocumentBuilder<ReviewsReport> reviewsContentBuilder,
//        IDocumentBuilder<DownloadsReport> downloadsContentBuilder,
//        IFormatter<AssetReport> formatter)
//    {
//        _salesContentBuilder = salesContentBuilder;
//        _reviewsContentBuilder = reviewsContentBuilder;
//        _downloadsContentBuilder = downloadsContentBuilder;
//        _formatter = formatter;
//    }

//    public string Build(AssetReport report, BuildSettings? settings = null)
//    {
//        if (settings.HasValue)
//        {
//            AdjustFormatting(settings.Value);
//        }

//        StringBuilder document = new();

//        AppendHeader(report, document);
//        AppendContent(report, document);

//        return document.ToString();
//    }

//    public void AdjustFormatting(BuildSettings settings)
//    {
//        _formatter.SetMargin(settings.Margin);
//    }

//    private void AppendHeader(AssetReport report, StringBuilder document)
//    {
//        document.AppendLine(value: _formatter.FormatLine($"ASSET {report.Asset.Name}\n"));
//    }

//    private void AppendContent(AssetReport report, StringBuilder document)
//    {
//        string separator = _formatter.ContentSeparator;

//        BuildSettings newParagraph = new()
//        {
//            Margin = _formatter.Options.Padding + 1
//        };

//        document.AppendJoin(
//            separator: '\n',
//            values:
//            [
//                separator,
//                _salesContentBuilder.Build(report.SalesReport, newParagraph),
//                separator,
//                _reviewsContentBuilder.Build(report.ReviewsReport, newParagraph),
//                separator,
//                _downloadsContentBuilder.Build(report.DownloadsReport, newParagraph)
//            ]);
//    }
//}
