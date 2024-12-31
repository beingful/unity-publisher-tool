using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Builders;

public class ReviewsDocumentBuilder : IDocumentBuilder<Reviews>
{
    private readonly IDocumentBuilder<Review> _contentBuilder;

    public ReviewsDocumentBuilder(IDocumentBuilder<Review> contentBuilder)
    {
        _contentBuilder = contentBuilder;
    }

    public IDocument Build(Reviews reviews)
    {
        Content content = new(
            text: Content(),
            formatting: new DocumentFormatting());

        IDocument document = Document.CreateParagraph(content);

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
