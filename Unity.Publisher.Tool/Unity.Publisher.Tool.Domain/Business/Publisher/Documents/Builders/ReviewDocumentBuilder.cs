using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public class ReviewDocumentBuilder : IDocumentBuilder<Review>
{
    public IDocument Build(Review review)
    {
        Content content = new(
            text: Content(review),
            formatting: new DocumentFormatting());

        return Document.CreateParagraph(content);
    }

    public string Content(Review review)
    {
        return $"Rating: {review.Rating}" +
            $"Subject: {review.Subject}" +
            $"Body: {review.Body}";
    }
}
