using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

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
