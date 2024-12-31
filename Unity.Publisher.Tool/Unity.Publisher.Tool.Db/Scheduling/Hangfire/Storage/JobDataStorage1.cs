//using Unity.Publisher.Tool.Infrastructure.Scheduling.Jobs;
//using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

//namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Storage;

//public class JobDataStorage1 : ISelfContainedJobDataStorage
//{
//    private readonly IJobIdProvider _jobIdProvider;
//    private readonly IJobParameterNameProvider<Type> _jobParameterNameProvider;
//    private readonly IJobDataStorage _storage;

//    public JobDataStorage1(
//        IJobIdProvider jobIdProvider,
//        IJobParameterNameProvider<Type> jobParameterNameProvider,
//        IJobDataStorage storage)
//    {
//        _jobIdProvider = jobIdProvider;
//        _jobParameterNameProvider = jobParameterNameProvider;
//        _storage = storage;
//    }

//    public void Set<TData>(TData data) where TData : notnull
//    {
//        _storage.Insert(
//            jobId: _jobIdProvider.Provide(),
//            data: new KeyValuePair<string, object>(
//                key: _jobParameterNameProvider.Provide(typeof(TData)),
//                value: data));
//    }

//    public TData Fetch<TData>()
//    {
//        return _storage.Fetch<TData>(
//            jobId: _jobIdProvider.Provide(),
//            parameterName: _jobParameterNameProvider.Provide(typeof(TData)));
//    }
//}
