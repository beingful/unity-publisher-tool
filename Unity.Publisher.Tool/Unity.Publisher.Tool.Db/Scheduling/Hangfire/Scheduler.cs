using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Coverters;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

public class Scheduler : IScheduler
{
    private readonly SchedulingOptions _schedulingOptions;
    private readonly IStorageConnection _storageConnection;
    private readonly ILogger<Scheduler> _logger;

    public Scheduler(
        IOptions<SchedulingOptions> schedulingOptions,
        IStorageConnection storageConnection,
        ILogger<Scheduler> logger)
    {
        _schedulingOptions = schedulingOptions.Value;
        _storageConnection = storageConnection;
        _logger = logger;
    }

    public void Schedule<TService>(Job job) where TService : IScheduleWorker
    {
        RecurringJob.AddOrUpdate<TService>(
            recurringJobId: job.Id,
            methodCall: (worker) => worker.ExecuteAsync(),
            cronExpression: TriggerTimeToCronConverter.Convert(job.Time),
            options: new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });
    }

    public void Unschedule(string jobId)
    {
        _storageConnection.GetJobData(jobId);
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

    private bool JobScheduled(string jobId)
    {
        return _storageConnection.GetRecurringJobs().Any(x => x.Id == jobId);
    }

    private bool SchedulePacked()
    {
        return _storageConnection.GetRecurringJobs().Count >= _schedulingOptions.JobsLimit;
    }
}
