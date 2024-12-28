namespace Unity.Publisher.Tool.Infrastructure.Db;

public interface IStorage
{
    ITransaction CreateTransaction();

    Task<TModel> GetAsync<TModel>(string id) where TModel : class;

    Task<TModel?> GetValueOrDefaultAsync<TModel>(string id) where TModel : class;

    Task UpdateAsync<TModel>(Entity<TModel> entity);

    Task UpsertAsync<TModel>(Entity<TModel> entity);

    Task InsertAsync<TModel>(Entity<TModel> entity);

    Task RemoveAsync<TModel>(string id);
}
