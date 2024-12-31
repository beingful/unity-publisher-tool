//using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
//using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;
//using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

//namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

//public class DataDrivenScheduler : IDataDrivenScheduler
//{
//    private readonly IScheduler _scheduler;
//    private readonly IScheduleStorage _schedulerStorage;

//    public DataDrivenScheduler(IScheduler scheduler, IScheduleStorage schedulerStorage)
//    {
//        _scheduler = scheduler;
//        _schedulerStorage = schedulerStorage;
//    }

//    public async Task ScheduleAsync<TService>(Job job) where TService : IScheduleWorker
//    {
//        if (_scheduler.CanSchedule(job.Id))
//        {
//            await DoBeforeSchedule(job.Id);

//            _scheduler.Schedule<TService>(job);
//        }
//        else
//        {
//            throw new Exception($"Job with id {job.Id} can not be scheduled.");
//        }
//    }

//    public async Task UnscheduleAsync<TData>(string jobId)
//    {
//        if (_scheduler.CanUnschedule(jobId))
//        {
//            _scheduler.Unschedule(jobId);

//            await DoAfterUnschedule<TData>(jobId);
//        }
//        else
//        {
//            throw new Exception($"Job with id {jobId} can not be unscheduled.");
//        }
//    }

//    public bool CanSchedule(string jobId)
//    {
//        return _scheduler.CanSchedule(jobId);
//    }

//    public bool CanUnschedule(string jobId)
//    {
//        return _scheduler.CanUnschedule(jobId);
//    }

//    private async Task DoBeforeSchedule<TData>(string jobId, TData data)
//    {
//        await _schedulerStorage.LoadAsync(jobId, data);
//    }

//    private async Task DoAfterUnschedule<TData>(string jobId)
//    {
//        await _schedulerStorage.UnloadAsync<TData>(jobId);
//    }
//}
