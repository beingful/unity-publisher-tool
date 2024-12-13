using System.Text;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class ReviewsDocumentBuilder : IDocumentBuilder<Reviews>
{
    private readonly IDocumentBuilder<Review> _contentBuilder;
    private readonly IFormatter<Reviews> _formatter;

    public ReviewsDocumentBuilder(
        IDocumentBuilder<Review> contentBuilder,
        IFormatter<Reviews> formatter)
    {
        _contentBuilder = contentBuilder;
        _formatter = formatter;
    }

    public string Build(Reviews reviews, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        StringBuilder document = new();

        AppendHeader(document);
        AppendContent(reviews, document);

        return document.ToString();
    }

    public void AdjustFormatting(BuildSettings settings)
    {
        _formatter.SetMargin(settings.Margin);
    }

    internal void AppendHeader(StringBuilder document, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        document.AppendLine(value: _formatter.FormatLine("NEW REVIEWS:\n"));
    }

    internal void AppendContent(Reviews reviews, StringBuilder document)
    {
        string separator = _formatter.ContentSeparator;

        _contentBuilder.AdjustFormatting(new BuildSettings
        {
            Margin = _formatter.Options.Padding + 1
        });

        for (int i = 0; i < reviews.Count; ++i)
        {
            document
                .AppendLine(value: _contentBuilder.Build(reviews[i]))
                .AppendLine(value: separator);
        }
    }
}
