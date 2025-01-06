namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

public interface INoRetryWorker<TData> : IScheduleWorker<TData>
{
    new Task ExecuteAsync(TData data);
}
