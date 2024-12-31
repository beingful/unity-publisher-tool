//using Hangfire.Storage;
//using Unity.Publisher.Tool.Infrastructure.Db;
//using Unity.Publisher.Tool.Infrastructure.Db.Redis;

//namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Storage;

//internal class ScheduleTransactionOld : IRedisTransaction
//{
//    private readonly IWriteOnlyTransaction _transaction;

//    public ScheduleTransactionOld(IWriteOnlyTransaction transaction)
//    {
//        _transaction = transaction;
//    }

//    public IRedisTransaction CreateTransaction()
//    {
//        return this;
//    }

//    public Task<TModel> GetAsync<TModel>(string id) where TModel : class
//    {
//        _transaction.
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

//    public Task ExecuteAsync()
//    {
//        _transaction.Commit();

//        return Task.CompletedTask;
//    }
//}