//using Unity.Publisher.Tool.Domain.Data;

//namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

//public abstract class ScheduleWorker<TData> : IScheduleWorker<TData> where TData : class
//{
//    private readonly IDataService<TData> _dataSource;
//    private readonly Predicate<TData> _canStartJob;
//    //private readonly IJobHandler<TData> _jobHandler;

//    public ScheduleWorker(IDataService<TData> dataSource, Predicate<TData> canStartJob/*, IJobHandler<TData> jobHandler*/)
//    {
//        _dataSource = dataSource;
//        //_jobHandler = jobHandler;
//    }

//    public async Task ExecuteAsync<TJobData>(TJobData jobData)
//    {
//        TData data = await _dataSource.GetAsync();

//        if (_canStartJob(data))
//        {
//            await StartJobAsync();
//        }
//        //await _jobHandler.HandleAsync(data);
//    }

//    protected abstract Task StartJobAsync<TJobData>(TData data, TJobData jobData);
//}
