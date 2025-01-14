namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

public interface IScheduleWorker<TData>
{
    Task ExecuteAsync(TData data);
}
