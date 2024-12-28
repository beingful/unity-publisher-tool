using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

public class ScheduleWorker<TData> : IScheduleWorker<TData> where TData : class
{
    private readonly IDataService<TData> _dataService;
    private readonly IHandler<TData> _dataHandler;

    public ScheduleWorker(IDataService<TData> dataService, IHandler<TData> dataHandler)
    {
        _dataService = dataService;
        _dataHandler = dataHandler;
    }

    public async Task ExecuteAsync()
    {
        TData data = await _dataService.GetAsync();

        await _dataHandler.HandleAsync(data);
    }
}
