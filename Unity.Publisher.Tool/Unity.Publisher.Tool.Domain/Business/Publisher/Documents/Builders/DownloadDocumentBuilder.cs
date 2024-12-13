using System.Text;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class DownloadDocumentBuilder : IDocumentBuilder<Download>
{
    private readonly IFormatter<Download> _formatter;

    public DownloadDocumentBuilder(IFormatter<Download> formatter)
    {
        _formatter = formatter;
    }

    public string Build(Download dowloads, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        StringBuilder document = new();

        AddHeader(document);
        AddContent(dowloads, document);

        return document.ToString();
    }

    public void AdjustFormatting(BuildSettings settings)
    {
        _formatter.SetMargin(settings.Margin);
    }

    private void AddHeader(StringBuilder document)
    {
        document.AppendLine(value: _formatter.FormatLine("NEW DOWNLOADS:\n"));
    }

    private void AddContent(Download download, StringBuilder document)
    {
        document.AppendLine(
            _formatter.FormatLines(
                $"Downloads: {download.Downloads}.",
                $"Downloaders: {download.Downloaders}."));
    }
}
