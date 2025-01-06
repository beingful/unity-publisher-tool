using Unity.Publisher.Tool.Domain.Notifications;
using Unity.Publisher.Tool.Domain.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherDocumentExporter<TData>
{
    private readonly IDocumentBuilder<TData> _documentBuilder;
    private readonly INotificator _notificator;

    public PublisherDocumentExporter(IDocumentBuilder<TData> documentBuilder, INotificator notificator)
    {
        _documentBuilder = documentBuilder;
        _notificator = notificator;
    }

    public async Task ExportAsync(TData content, Sender sender, Receiver receiver)
    {
        IDocument document = _documentBuilder.Build(content);

        Message message = new()
        {
            Subject = $"Unity asset store publisher event: {document.Title.Description}",
            Body = document.ToString()
        };

        await _notificator
            .SendAsync(sender, receiver, message, CancellationToken.None)
            .ConfigureAwait(false);
    }
}
