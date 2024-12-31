using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherEventHandler<TData> : IScheduleWorker<NotificationDetails>
    where TData : class
{
    private readonly IDataService<TData> _dataService;
    private readonly Predicate<TData> _eventOccured;
    private readonly DocumentExporter<TData> _documentExporter;

    public PublisherEventHandler(
        IDataService<TData> dataService,
        Predicate<TData> eventOccured,
        DocumentExporter<TData> documentExporter)
    {
        _dataService = dataService;
        _eventOccured = eventOccured;
        _documentExporter = documentExporter;
    }

    public async Task ExecuteAsync(NotificationDetails notificationDetails)
    {
        TData data = await _dataService.GetAsync();

        if (_eventOccured(data))
        {
            await _documentExporter.ExportAsync(
                data, notificationDetails.Sender, notificationDetails.Receiver);
        }
    }
}
