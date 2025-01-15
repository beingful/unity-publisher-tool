namespace Unity.Publisher.Tool.Domain.General;

public interface IDataSource<TData> where TData : class
{
    Task<TData> GetAsync();
}
