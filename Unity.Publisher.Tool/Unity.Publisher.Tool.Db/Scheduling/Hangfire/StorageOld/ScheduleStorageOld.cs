//using Hangfire.Storage;
//using Unity.Publisher.Tool.Infrastructure.Db;
//using Unity.Publisher.Tool.Infrastructure.Db.Redis;
//using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

//namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Storage;

//public class ScheduleStorageOld : IScheduleStorage
//{
//    private readonly IStorageConnection _storageConnection;

//    public ScheduleStorageOld(IStorageConnection storageConnection)
//    {
//        _storageConnection = storageConnection;
//    }

//    public IRedisTransaction CreateTransaction()
//    {
//        throw new NotImplementedException();
//    }

//    public Task<TModel> GetAsync<TModel>(string id) where TModel : class
//    {
//        throw new NotImplementedException();
//    }

//    public Task<TModel?> GetValueOrDefaultAsync<TModel>(string id) where TModel : class
//    {
//        throw new NotImplementedException();
//    }

//    public Task InsertAsync<TModel>(Entity<TModel> entity)
//    {
//        throw new NotImplementedException();
//    }

//    public Task RemoveAsync<TModel>(string id)
//    {
//        throw new NotImplementedException();
//    }

//    public Task UpdateAsync<TModel>(Entity<TModel> entity)
//    {
//        throw new NotImplementedException();
//    }

//    public Task UpsertAsync<TModel>(Entity<TModel> entity)
//    {
//        throw new NotImplementedException();
//    }
//}
