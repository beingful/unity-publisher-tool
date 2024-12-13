namespace Unity.Publisher.Tool.Domain.Data;

public interface IDataSource<TData> where TData : class
{
    Task<TData> GetAsync();
}
