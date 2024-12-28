using Unity.Publisher.Tool.Infrastructure.Db;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

public class ScheduleStorage : IScheduleStorage
{
    private readonly IStorage _storage;

    public ScheduleStorage(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<TSchedulerData> FetchAsync<TSchedulerData>(string jobId)
        where TSchedulerData : class
    {
        return await _storage.GetAsync<TSchedulerData>(jobId);
    }

    public async Task LoadAsync<TSchedulerData>(string jobId, TSchedulerData data)
    {
        await _storage.InsertAsync(new Entity<TSchedulerData>
        {
            Id = jobId,
            Data = data
        });
    }

    public async Task UnloadAsync<TSchedulerData>(string jobId)
    {
        await _storage.RemoveAsync<TSchedulerData>(jobId);
    }
}
