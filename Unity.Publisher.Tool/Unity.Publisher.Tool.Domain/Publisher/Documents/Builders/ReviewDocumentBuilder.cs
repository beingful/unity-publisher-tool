using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class ReviewDocumentBuilder : IDocumentBuilder<Review>
{
    public IDocument Build(Review review)
    {
        Content content = new(
            text: Content(review),
            formatting: new ParagraphFormatting());

        return Document.Create(content);
    }

    public string Content(Review review)
    {
        return $"Rating: {review.Rating}\n\n" +
            $"Subject: {review.Subject}\n\n" +
            $"Body: {review.Body}";
    }
}
