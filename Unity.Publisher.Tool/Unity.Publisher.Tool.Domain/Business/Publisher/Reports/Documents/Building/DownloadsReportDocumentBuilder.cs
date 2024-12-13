//using System.Text;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Documents.Building;

//public class DownloadsReportDocumentBuilder : IDocumentBuilder<DownloadsReport>
//{
//    private readonly IDocumentBuilder<Download> _contentBuilder;
//    private readonly IFormatter<DownloadsReport> _formatter;

//    public DownloadsReportDocumentBuilder(
//        IDocumentBuilder<Download> contentBuilder,
//        IFormatter<DownloadsReport> formatter)
//    {
//        _contentBuilder = contentBuilder;
//        _formatter = formatter;
//    }

//    public string Build(DownloadsReport report, BuildSettings? settings = null)
//    {
//        if (settings.HasValue)
//        {
//            AdjustFormatting(settings.Value);
//        }

//        StringBuilder document = new();

//        AddHeader(document);
//        AddContent(report.Download, document);

//        return document.ToString();
//    }

//    public void AdjustFormatting(BuildSettings settings)
//    {
//        _formatter.SetMargin(settings.Margin);
//    }

//    private void AddHeader(StringBuilder document)
//    {
//        document.AppendLine(value: _formatter.FormatLine("DOWNLOADS:\n"));
//    }

//    private void AddContent(Download download, StringBuilder document)
//    {
//        document.AppendLine(value:
//            _contentBuilder.Build(download, new BuildSettings
//            {
//                Margin = _formatter.Options.Padding + 1
//            }));
//    }
//}
