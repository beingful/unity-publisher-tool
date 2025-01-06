using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public abstract class PublisherReportingService<TData> : IPublisherReportingService
    where TData : class
{
    private readonly IDataSource<TData> _dataSource;
    private readonly PublisherDocumentExporter<TData> _documentExporter;

    public PublisherReportingService(
        IDataSource<TData> dataSource,
        PublisherDocumentExporter<TData> documentExporter)
    {
        _dataSource = dataSource;
        _documentExporter = documentExporter;
    }

    public async Task ReportAsync(Sender sender, Receiver receiver)
    {
        TData data = await _dataSource.GetAsync();

        if (CanReport(data))
        {
            await _documentExporter.ExportAsync(data, sender, receiver);
        }
    }

    protected abstract bool CanReport(TData data);
}
