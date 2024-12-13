using Unity.Publisher.Tool.Infrastructure.Db.Entities;

namespace Unity.Publisher.Tool.Infrastructure.Db;

public interface IStorage
{
    ITransaction CreateTransaction();

    Task<TValue> GetAsync<TValue>(string id) where TValue : BaseEntity;

    Task<TValue?> GetValueOrDefaultAsync<TValue>(string id) where TValue : BaseEntity;

    Task InsertAsync<TValue>(TValue value) where TValue : BaseEntity;

    Task UpdateAsync<TValue>(TValue value) where TValue : BaseEntity;

    Task UpsertAsync<TValue>(TValue value) where TValue : BaseEntity;

    Task RemoveAsync<TValue>(string id) where TValue : BaseEntity;

    //Task DoAllAsync(params Func<IDbRepository, Task>[] tasks);
}
