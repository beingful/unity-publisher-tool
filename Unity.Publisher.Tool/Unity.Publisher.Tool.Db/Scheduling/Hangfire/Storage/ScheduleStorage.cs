//using Hangfire.Storage;
//using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

//namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Storage;

//public class ScheduleStorage : IScheduleStorage
//{
//    private readonly IStorageConnection _storageConnection;

//    public ScheduleStorage(IStorageConnection storageConnection)
//    {
//        _storageConnection = storageConnection;
//    }

//    public bool Contains(string jobId)
//    {
//        return _storageConnection.GetRecurringJobs().Any(x => x.Id == jobId);
//    }

//    public int Occupation()
//    {
//        return _storageConnection.GetRecurringJobs().Count;
//    }
//}
