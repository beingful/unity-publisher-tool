using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class ReviewsDocumentBuilder : IDocumentBuilder<Reviews>
{
    private readonly IDocumentBuilder<Review> _contentBuilder;

    public ReviewsDocumentBuilder(IDocumentBuilder<Review> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(Reviews reviews)
    {
        Metadata metadata = Metadata.WithDescription("REVIEWS");

        Content content = new(
            text: Content(),
            formatting: new ParagraphFormatting(new FormattingOptions
            {
                Separator = '*'
            }));

        Document document = Document.Create(content, metadata);

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
