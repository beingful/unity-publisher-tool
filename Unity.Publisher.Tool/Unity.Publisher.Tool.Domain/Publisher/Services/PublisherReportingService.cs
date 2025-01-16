using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public abstract class PublisherReportingService<TData> : IPublisherReportingService
    where TData : class
{
    private readonly IDataSource<TData> _dataSource;
    private readonly PublisherDocumentExporter<TData> _documentExporter;
    private readonly ILogger _logger;

    public PublisherReportingService(
        IDataSource<TData> dataSource,
        PublisherDocumentExporter<TData> documentExporter,
        ILogger<PublisherReportingService<TData>> logger)
    {
        _dataSource = dataSource;
        _documentExporter = documentExporter;
        _logger = logger;
    }

    public async Task ReportAsync(Sender sender, Receiver receiver, CancellationToken cancellationToken = default)
    {
        TData data = await _dataSource.GetAsync();

        if (CanReport(data))
        {
            await _documentExporter.ExportAsync(data, sender, receiver, cancellationToken);
        }
        else
        {
            _logger.LogInformation("The document should not be reported.");
        }
    }

    protected abstract bool CanReport(TData data);
}
