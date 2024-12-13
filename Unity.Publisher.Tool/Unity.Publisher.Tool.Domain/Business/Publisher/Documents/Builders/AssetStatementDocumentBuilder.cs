using System.Text;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class AssetStatementDocumentBuilder : IDocumentBuilder<AssetStatement>
{
    private readonly IDocumentBuilder<Sales> _salesContentBuilder;
    private readonly IDocumentBuilder<Reviews> _reviewsContentBuilder;
    private readonly IDocumentBuilder<Download> _downloadsContentBuilder;
    private readonly IFormatter<AssetStatement> _formatter;

    public AssetStatementDocumentBuilder(
        IDocumentBuilder<Sales> salesContentBuilder,
        IDocumentBuilder<Reviews> reviewsContentBuilder,
        IDocumentBuilder<Download> downloadsContentBuilder,
        IFormatter<AssetStatement> formatter)
    {
        _salesContentBuilder = salesContentBuilder;
        _reviewsContentBuilder = reviewsContentBuilder;
        _downloadsContentBuilder = downloadsContentBuilder;
        _formatter = formatter;
    }

    public string Build(AssetStatement statement, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        StringBuilder document = new();

        AppendHeader(statement, document);
        AppendContent(statement, document);

        return document.ToString();
    }

    public void AdjustFormatting(BuildSettings settings)
    {
        _formatter.SetMargin(settings.Margin);
    }

    private void AppendHeader(AssetStatement statement, StringBuilder document)
    {
        document.AppendLine(value: _formatter.FormatLine($"{statement.Asset.Name.ToUpper()}\n"));
    }

    private void AppendContent(AssetStatement statement, StringBuilder document)
    {
        string separator = _formatter.ContentSeparator;

        BuildSettings newParagraph = new()
        {
            Margin = _formatter.Options.Padding + 1
        };

        if (statement.Sales.IsEmpty == false)
        {
            AppendContent(statement.Sales, _salesContentBuilder, newParagraph, separator, document);
        }

        if (statement.Reviews.IsEmpty == false)
        {
            AppendContent(statement.Reviews, _reviewsContentBuilder, newParagraph, separator, document);
        }

        if (statement.Downloads.IsEmpty == false)
        {
            AppendContent(statement.Downloads, _downloadsContentBuilder, newParagraph, separator, document);
        }
    }

    private void AppendContent<TContent>(
        TContent content, IDocumentBuilder<TContent> contentBuilder,
        BuildSettings contentBuildSettings, string separator, StringBuilder document)
    {
        document
            .AppendLine(contentBuilder.Build(content, contentBuildSettings))
            .AppendLine(separator);
    }
}
