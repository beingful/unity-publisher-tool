using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public interface IPublisherDocumentExporter<TData>
{
    Task ExportAsync(TData content, Sender sender, Receiver receiver, CancellationToken cancellationToken = default);
}
