using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Notifications;
using Unity.Publisher.Tool.Domain.Publisher.Services.Reports;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherMessageService<TData> : IPublisherMessageService where TData : class
{
    private readonly IDataSource<TData> _dataSource;
    private readonly IPublisherDocumentExporter<TData> _documentExporter;
    private readonly ILogger _logger;

    public PublisherMessageService(
        IDataSource<TData> dataSource,
        IPublisherDocumentExporter<TData> documentExporter,
        ILogger<PublisherMessageService<TData>> logger)
    {
        _dataSource = dataSource;
        _documentExporter = documentExporter;
        _logger = logger;
    }

    public async Task MessageAsync(Sender sender, Receiver receiver, CancellationToken cancellationToken = default)
    {
        TData data = await _dataSource.GetAsync(cancellationToken);

        if (CanMessage(data))
        {
            await _documentExporter.ExportAsync(data, sender, receiver, cancellationToken);
        }
        else
        {
            _logger.LogInformation("The document should not be reported.");
        }
    }

    protected virtual bool CanMessage(TData data)
    {
        return true;
    }
}
