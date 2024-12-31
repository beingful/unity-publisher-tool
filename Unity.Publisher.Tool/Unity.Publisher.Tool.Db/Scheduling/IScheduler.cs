using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public interface IScheduler
{
    void Schedule<TWorker, TWorkerArg>(Job job, TWorkerArg argument) where TWorker : IScheduleWorker<TWorkerArg>;

    void Unschedule(string jobId);

    bool CanSchedule(string jobId);

    bool CanUnschedule(string jobId);
}
