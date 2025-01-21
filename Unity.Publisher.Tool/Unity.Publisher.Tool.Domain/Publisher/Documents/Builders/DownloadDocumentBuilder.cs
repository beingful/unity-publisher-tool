using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public class DownloadDocumentBuilder : IDocumentBuilder<Download>
{
    public IDocument Build(Download dowload)
    {
        Metadata metadata = Metadata.WithDescription("DOWNLOADS");

        Content content = new(
            text: Content(dowload),
            formatting: new ParagraphFormatting());

        return Document
            .Create(content, metadata)
            .AddInner(InnerDocument(dowload));
    }

    private string Content(Download download)
    {
        return "NEW DOWNLOADS:\n";
    }

    private IDocument InnerDocument(Download download)
    {
        return Document.Create(
            content: new Content(
                text: $"Downloads: {download.Downloads}.\n" +
                      $"Downloaders: {download.Downloaders}.",
                formatting: new ParagraphFormatting()));
    }
}
