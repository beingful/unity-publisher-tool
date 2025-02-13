namespace Unity.Publisher.Tool.Domain.General;

public interface ITimeDependentDataSource<TData> where TData : class
{
    Task<TData> GetAsync(DateTime time, CancellationToken cancellationToken = default);
}
