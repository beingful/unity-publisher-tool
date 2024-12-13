using System.Text;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Documents.Building;

public class PublisherReportDocumentBuilder : IDocumentBuilder<PublisherReport>
{
    private readonly IDocumentBuilder<PublisherStatement> _contentBuilder;
    private readonly IFormatter<PublisherStatement> _formatter;

    public PublisherReportDocumentBuilder(
        IDocumentBuilder<PublisherStatement> contentBuilder,
        IFormatter<PublisherStatement> formatter)
    {
        _contentBuilder = contentBuilder;
        _formatter = formatter;
    }

    public string Build(PublisherReport report, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        StringBuilder document = new();

        AppendHead(report, document);
        AppendContent(report.Statement, document);

        return document.ToString();
    }

    public void AdjustFormatting(BuildSettings settings)
    {
        _formatter.SetMargin(settings.Margin);
    }

    private void AppendHead(PublisherReport report, StringBuilder document)
    {
        document.AppendLine(
            value: _formatter.FormatLines(
                "REPORT\n",
                $"Publisher: {report.Publisher.Name}",
                $"Publisher rating: {report.Publisher.Rating.Average}",
                $"This month revenue: {report.Revenue.ForPeriod}",
                $"Total revenue: {report.Revenue.Total}",
                $"Month reported: {report.Month.Name}"));
    }

    private void AppendContent(PublisherStatement statement, StringBuilder document)
    {
        string separator = _formatter.ContentSeparator;

        document
            .AppendLine(_contentBuilder.Build(statement, new BuildSettings
            {
                Margin = _formatter.Options.Padding + 1
            }))
            .AppendLine(separator);
    }
}
