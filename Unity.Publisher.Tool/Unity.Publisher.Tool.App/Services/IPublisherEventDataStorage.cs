namespace Unity.Publisher.Tool.App.Services;

public interface IPublisherEventDataStorage
{
    void Set<TData>(TData data) where TData : notnull;

    TData Fetch<TData>();
}
