using Unity.Publisher.Tool.Domain.Business.Publisher.Documents;
using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
using Unity.Publisher.Tool.Infrastructure.Notification;
using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.App.Services;

public class DocumentExporter<TData>
{
    private readonly IDocumentBuilder<TData> _documentBuilder;
    private readonly INotificator _notificator;

    public DocumentExporter(IDocumentBuilder<TData> documentBuilder, INotificator notificator)
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
