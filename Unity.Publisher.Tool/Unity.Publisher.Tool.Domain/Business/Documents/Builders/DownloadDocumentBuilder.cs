using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Builders;

public class DownloadDocumentBuilder : IDocumentBuilder<Download>
{
    public IDocument Build(Download dowloads)
    {
        Content content = new(
            text: Content(dowloads),
            formatting: new DocumentFormatting());

        return Document.CreateParagraph(content);
    }

    private string Content(Download download)
    {
        return "NEW DOWNLOADS:\n\n" +
            $"Downloads: {download.Downloads}.\n" +
            $"Downloaders: {download.Downloaders}.";
    }
}
