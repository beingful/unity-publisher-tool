namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

public interface IScheduleWorker
{
    Task ExecuteAsync();
}

public interface IScheduleWorker<TContent> : IScheduleWorker;
