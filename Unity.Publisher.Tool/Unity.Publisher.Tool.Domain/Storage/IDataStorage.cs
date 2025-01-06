namespace Unity.Publisher.Tool.Domain.Storage;

public interface IDataStorage
{
    void Set<TData>(TData data) where TData : class;

    TData? Fetch<TData>() where TData : class;
}
