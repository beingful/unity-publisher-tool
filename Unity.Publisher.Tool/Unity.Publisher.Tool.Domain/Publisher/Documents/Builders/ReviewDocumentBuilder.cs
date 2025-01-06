using Unity.Publisher.Tool.Domain.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class ReviewDocumentBuilder : IDocumentParagraphBuilder<Review>
{
    public IDocument Build(Review review)
    {
        Content content = new(
            text: Content(review),
            formatting: new ParagraphFormatting());

        return Document.CreateParagraph(content);
    }

    public string Content(Review review)
    {
        return $"Rating: {review.Rating}" +
            $"Subject: {review.Subject}" +
            $"Body: {review.Body}";
    }
}
