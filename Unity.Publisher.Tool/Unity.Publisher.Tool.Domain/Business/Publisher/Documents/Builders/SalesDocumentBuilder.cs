using System.Text;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class SalesDocumentBuilder : IDocumentBuilder<Sales>
{
    private readonly IDocumentBuilder<Sale> _contentBuilder;
    private readonly IFormatter<Sales> _formatter;

    public SalesDocumentBuilder(
        IDocumentBuilder<Sale> contentBuilder,
        IFormatter<Sales> formatter)
    {
        _contentBuilder = contentBuilder;
        _formatter = formatter;
    }

    public string Build(Sales sales, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        StringBuilder document = new();

        AppendHeader(document);
        AppendContent(sales, document);
        AppendFooter(sales, document);

        return document.ToString();
    }

    public void AdjustFormatting(BuildSettings settings)
    {
        _formatter.SetMargin(settings.Margin);
    }

    private void AppendHeader(StringBuilder document)
    {
        document.AppendLine(_formatter.FormatLine("NEW SALES:\n"));
    }

    private void AppendContent(Sales sales, StringBuilder document)
    {
        string separator = _formatter.ContentSeparator;

        _contentBuilder.AdjustFormatting(new BuildSettings
        {
            Margin = _formatter.Options.Padding + 1
        });

        for (int i = 0; i < sales.Count; ++i)
        {
            document
                .AppendLine(_contentBuilder.Build(sales[i]))
                .AppendLine(separator);
        }
    }

    private void AppendFooter(Sales sales, StringBuilder document)
    {
        document.AppendLine(_formatter.FormatLine($"Revenue: ${sales.Revenue}.\n"));
    }
}
