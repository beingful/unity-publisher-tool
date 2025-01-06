using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

public class Scheduler : IScheduler
{
    private readonly IStorageConnection _storageConnection;
    private readonly ILogger<Scheduler> _logger;
    private readonly SchedulingOptions _options;

    public Scheduler(
        IStorageConnection storageConnection,
        ILogger<Scheduler> logger,
        IOptions<SchedulingOptions> options)
    {
        _storageConnection = storageConnection;
        _logger = logger;
        _options = options.Value;
    }

    public void Schedule<TWorker, TWorkerData>(Job job, TWorkerData data)
        where TWorker : IScheduleWorker<TWorkerData>
    {
        RecurringJob.AddOrUpdate<TWorker>(
            recurringJobId: job.Id,
            methodCall: worker => worker.ExecuteAsync(data),
            cronExpression: TriggerTimeToCronConverter.Convert(job.Time),
            options: new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });
    }

    public void Unschedule(string jobId)
    {
        RecurringJob.RemoveIfExists(jobId);
    }

    public bool CanSchedule(string jobId)
    {
        return (SchedulePacked() || JobScheduled(jobId)) == false;
    }

    public bool CanUnschedule(string jobId)
    {
        return JobScheduled(jobId);
    }

    private bool SchedulePacked()
    {
        return _storageConnection.GetRecurringJobs().Count >= _options.JobsLimit;
    }

    private bool JobScheduled(string jobId)
    {
        return _storageConnection.GetRecurringJobs().Any(x => x.Id == jobId);
    }
}
