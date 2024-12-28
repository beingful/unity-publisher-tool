using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public interface IScheduler
{
    void Schedule<TService>(Job job) where TService : IScheduleWorker;

    void Unschedule(string jobId);

    bool CanSchedule(string jobId);

    bool CanUnschedule(string jobId);
}
