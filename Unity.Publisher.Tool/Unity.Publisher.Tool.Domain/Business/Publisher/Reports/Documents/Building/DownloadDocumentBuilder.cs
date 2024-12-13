//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Documents.Building;

//public class DownloadDocumentBuilder : IDocumentBuilder<Download>
//{
//    private readonly IFormatter<Download> _formatter;

//    public DownloadDocumentBuilder(IFormatter<Download> formatter)
//    {
//        _formatter = formatter;
//    }

//    public string Build(Download download, BuildSettings? settings = null)
//    {
//        if (settings.HasValue)
//        {
//            AdjustFormatting(settings.Value);
//        }

//        return _formatter.FormatLines(
//           $"Downloaders: {download.Downloaders}.",
//           $"Downloads: {download.Downloads}.");
//    }

//    public void AdjustFormatting(BuildSettings settings)
//    {
//        _formatter.SetMargin(settings.Margin);
//    }
//}
