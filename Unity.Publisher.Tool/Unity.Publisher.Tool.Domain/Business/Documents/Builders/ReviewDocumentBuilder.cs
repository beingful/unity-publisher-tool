using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Builders;

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
