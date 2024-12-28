using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public interface IDataDrivenScheduler
{
    Task ScheduleAsync<TService, TData>(Job job, TData data) where TService : IScheduleWorker;

    Task UnscheduleAsync<TData>(string jobId);

    bool CanSchedule(string jobId);

    bool CanUnschedule(string jobId);
}
