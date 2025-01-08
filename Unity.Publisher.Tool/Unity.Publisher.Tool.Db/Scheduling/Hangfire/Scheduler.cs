using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Options;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

public class Scheduler : IScheduler
{
    private readonly SchedulingOptions _options;
    private readonly ILogger<Scheduler> _logger;

    public Scheduler(IOptions<SchedulingOptions> options, ILogger<Scheduler> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public void Schedule<TWorker, TWorkerData>(Job job, TWorkerData data)
        where TWorker : IScheduleWorker<TWorkerData>
    {
        using IStorageConnection storage = JobStorage.Current.GetConnection();

        List<RecurringJobDto> jobs = storage.GetRecurringJobs();

        if (JobScheduled(job.Id, jobs))
        {
            throw new Exception(message: "The job is already scheduled.");
        }
        else if (SchedulePacked(jobs))
        {
            throw new Exception(message: $"Schedule is packed with {_options.JobsLimit} jobs.");
        }

        RecurringJob.AddOrUpdate<TWorker>(
            recurringJobId: job.Id,
            methodCall: worker => worker.ExecuteAsync(data),
            cronExpression: TriggerTimeToCronConverter.Convert(job.TriggerTime),
            options: new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });
    }

    public void Unschedule(string jobId)
    {
        using IStorageConnection storage = JobStorage.Current.GetConnection();

        List<RecurringJobDto> jobs = storage.GetRecurringJobs();

        if (JobScheduled(jobId, jobs) == false)
        {
            throw new Exception(message: "The job either is already unscheduled or was never scheduled.");
        }

        using (storage.AcquireDistributedJobLock(jobId, TimeSpan.FromSeconds(5)))
        {
            using IWriteOnlyTransaction transaction = storage.CreateWriteTransaction();

            transaction.RemoveHash($"job:{jobId}");
            transaction.RemoveHash($"recurring-job:{jobId}");
            transaction.RemoveFromSet("recurring-jobs", jobId);
            transaction.Commit();
        }
    }

    private bool JobScheduled(string jobId, List<RecurringJobDto> jobs)
    {
        return jobs.Any(x => x.Id == jobId);
    }

    private bool SchedulePacked(List<RecurringJobDto> jobs)
    {
        return jobs.Count >= _options.JobsLimit;
    }
}
