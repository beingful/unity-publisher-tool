using System.Text;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class PublisherStatementDocumentBuilder : IDocumentBuilder<PublisherStatement>
{
    private readonly IDocumentBuilder<AssetStatement> _contentBuilder;
    private readonly IFormatter<PublisherStatement> _formatter;

    public PublisherStatementDocumentBuilder(
        IDocumentBuilder<AssetStatement> contentBuilder,
        IFormatter<PublisherStatement> formatter)
    {
        _contentBuilder = contentBuilder;
        _formatter = formatter;
    }

    public string Build(PublisherStatement statement, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        StringBuilder document = new();

        AppendHead(statement, document);
        AppendContent(statement.AssetsStatements, document);

        return document.ToString();
    }

    public void AdjustFormatting(BuildSettings settings)
    {
        _formatter.SetMargin(settings.Margin);
    }

    internal void AppendHead(PublisherStatement report, StringBuilder document)
    {
        document.AppendLine(_formatter.FormatLines("LATEST UPDATES:\n"));
    }

    private void AppendContent(AssetStatement[] assetStatements, StringBuilder document)
    {
        string separator = _formatter.ContentSeparator;

        _contentBuilder.AdjustFormatting(new BuildSettings
        {
            Margin = _formatter.Options.Padding + 1
        });

        for (int i = 0; i < assetStatements.Length; ++i)
        {
            document
                .AppendLine(value: _contentBuilder.Build(assetStatements[i]))
                .AppendLine(value: separator);
        }
    }
}
