using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class PublisherInfoDocumentBuilder : IParagraphBuilder<PublisherInfo>
{
    public IDocument Build(PublisherInfo publisher)
    {
        Content content = new(
            text: Content(publisher),
            formatting: new ParagraphFormatting());

        return Document.CreateParagraph(content);
    }

    private string Content(PublisherInfo publisher)
    {
        return $"Publisher: {publisher.Name}.\n" +
            $"Publisher rating: {publisher.Rating}.";
    }
}
