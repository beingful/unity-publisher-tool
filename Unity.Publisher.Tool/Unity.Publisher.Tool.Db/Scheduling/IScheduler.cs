using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public interface IScheduler
{
    void Schedule<TService>(Job job) where TService : IScheduleWorker;

    void Unschedule(string jobId);

    bool CanSchedule(string jobId);

    bool CanUnschedule(string jobId);
}
