namespace Unity.Publisher.Tool.Domain.Data;

public interface IDataService<TData> where TData : class
{
    Task<TData> GetAsync();
}
