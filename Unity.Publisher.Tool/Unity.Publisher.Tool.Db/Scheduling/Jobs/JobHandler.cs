//using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

//namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Jobs;

//public abstract class JobHandler<TData> : IJobHandler<TData>
//{
//    private readonly Predicate<TData> _canHandle;
//    private readonly ISelfContainedJobDataStorage _storage;

//    public JobHandler(Predicate<TData> canHandle, ISelfContainedJobDataStorage storage)
//    {
//        _canHandle = canHandle;
//        _storage = storage;
//    }

//    public async Task HandleAsync(TData data)
//    {
//        if (_canHandle(data))
//        {
//            await StartJobAsync(data);
//        }
//    }

//    protected abstract Task StartJobAsync(TData data);

//    protected TJobData GetJobData<TJobData>()
//    {
//        return _storage.Fetch<TJobData>();
//    }
//}
