using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class ReviewDocumentBuilder : IDocumentBuilder<Review>
{
    private readonly IFormatter<Review> _formatter;

    public ReviewDocumentBuilder(IFormatter<Review> formatter)
    {
        _formatter = formatter;
    }

    public string Build(Review review, BuildSettings? settings = null)
    {
        if (settings.HasValue)
        {
            AdjustFormatting(settings.Value);
        }

        return _formatter.FormatLines(
            $"Rating: {review.Rating}",
            $"Subject: {review.Subject}",
            $"Body: {review.Body}");
    }

    public void AdjustFormatting(BuildSettings settings)
    {
        _formatter.SetMargin(settings.Margin);
    }
}
