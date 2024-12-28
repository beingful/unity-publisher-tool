using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherEventNotificationService<TData> : IHandler<TData>
{
    private readonly IProvider<string> _idProvider;
    private readonly DocumentExporter<TData> _documentExporter;
    private readonly IScheduleStorage _storage;

    public PublisherEventNotificationService(
        IProvider<string> idProvider,
        DocumentExporter<TData> documentExporter,
        IScheduleStorage storage)
    {
        _idProvider = idProvider;
        _documentExporter = documentExporter;
        _storage = storage;
    }

    public async Task HandleAsync(TData data)
    {
        NotificationJobData jobData = await _storage
            .FetchAsync<NotificationJobData>(jobId: _idProvider.Provide());

        await _documentExporter.ExportAsync(data, jobData.Sender, jobData.Receiver);
    }
}
