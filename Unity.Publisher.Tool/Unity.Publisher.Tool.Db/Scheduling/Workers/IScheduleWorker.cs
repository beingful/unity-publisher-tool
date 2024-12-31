namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

public interface IScheduleWorker<TJobData>
{
    Task ExecuteAsync(TJobData data);
}
