namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public interface IScheduleWorker
{
    Task ExecuteAsync();
}

public interface IScheduleWorker<TExecutor> : IScheduleWorker
{
}
