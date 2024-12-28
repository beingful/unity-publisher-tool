namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

public interface IScheduleStorage
{
    Task<TSchedulerData> FetchAsync<TSchedulerData>(string jobId) where TSchedulerData : class;

    Task LoadAsync<TSchedulerData>(string jobId, TSchedulerData data);

    Task UnloadAsync<TSchedulerData>(string jobId);
}
