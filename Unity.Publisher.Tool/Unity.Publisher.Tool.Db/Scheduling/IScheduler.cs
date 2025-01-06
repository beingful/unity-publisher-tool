using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public interface IScheduler
{
    void Schedule<TWorker, TWorkerData>(Job job, TWorkerData workerData)
        where TWorker : IScheduleWorker<TWorkerData>;

    void Unschedule(string jobId);

    bool CanSchedule(string jobId);

    bool CanUnschedule(string jobId);
}
