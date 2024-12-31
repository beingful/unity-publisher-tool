using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Coverters;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Hangfire.Storage;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

public class Scheduler : IScheduler
{
    //private readonly IJobDataStorage _jobDataStorage;
    //private readonly IScheduleStorage _scheduleStorage;
    private readonly IStorageConnection _storageConnection;
    private readonly ILogger<Scheduler> _logger;
    private readonly SchedulingOptions _options;

    public Scheduler(
        //IJobDataStorage jobDataStorage,
        //IScheduleStorage scheduleStorage,
        IStorageConnection storageConnection,
        ILogger<Scheduler> logger,
        IOptions<SchedulingOptions> options)
    {
        //_jobDataStorage = jobDataStorage;
        //_scheduleStorage = scheduleStorage;
        _storageConnection = storageConnection;
        _logger = logger;
        _options = options.Value;
    }

    public void Schedule<TWorker, TWorkerArg>(Job job, TWorkerArg argument)
        where TWorker : IScheduleWorker<TWorkerArg>
    {
        RecurringJob.AddOrUpdate<TWorker>(
            recurringJobId: job.Id,
            methodCall: worker => worker.ExecuteAsync(argument),
            cronExpression: TriggerTimeToCronConverter.Convert(job.Time),
            options: new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });

        //if (job.IsParameterless == false)
        //{
        //    _jobDataStorage.Insert(job.Id, job.Parameters);
        //}
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
