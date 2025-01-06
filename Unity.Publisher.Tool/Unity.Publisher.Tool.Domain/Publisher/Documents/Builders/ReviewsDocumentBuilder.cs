using Unity.Publisher.Tool.Domain.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class ReviewsDocumentBuilder : IDocumentParagraphBuilder<Reviews>
{
    private readonly IDocumentParagraphBuilder<Review> _contentBuilder;

    public ReviewsDocumentBuilder(IDocumentParagraphBuilder<Review> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(Reviews reviews)
    {
        Content content = new(
            text: Content(),
            formatting: new ParagraphFormatting());

        Document document = Document.CreateParagraph(content);

        InnerDocuments(reviews).ForEach(inner => document.AddInner(inner));

        return document;
    }

    private string Content()
    {
        return "NEW REVIEWS:\n";
    }

    internal List<IDocument> InnerDocuments(Reviews reviews)
    {
        List<IDocument> documents = new(reviews.Count);

        for (int i = 0; i < reviews.Count; ++i)
        {
            documents.Add(_contentBuilder.Build(reviews[i]));
        }

        return documents;
    }
}
