using Unity.Publisher.Tool.Domain.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class DownloadDocumentBuilder : IDocumentParagraphBuilder<Download>
{
    public IDocument Build(Download dowload)
    {
        Content content = new(
            text: Content(dowload),
            formatting: new ParagraphFormatting());

        return Document
            .CreateParagraph(content)
            .AddInner(InnerDocument(dowload));
    }

    private string Content(Download download)
    {
        return "NEW DOWNLOADS:\n";
    }

    private IDocument InnerDocument(Download download)
    {
        return Document.CreateParagraph(
            content: new Content(
                text: $"Downloads: {download.Downloads}.\n" +
                      $"Downloaders: {download.Downloaders}.",
                formatting: new ParagraphFormatting()));
    }
}
