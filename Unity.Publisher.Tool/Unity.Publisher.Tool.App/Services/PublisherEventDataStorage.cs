using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherEventDataStorage : IPublisherEventDataStorage
{
    private readonly IPublisherEventIdProvider _publisherEventIdProvider;
    private readonly IPublisherEventDataNameProvider<Type> _publisherEventDataNameProvider;
    private readonly IJobDataStorage _storage;

    public PublisherEventDataStorage(
        IPublisherEventIdProvider publisherEventIdProvider,
        IPublisherEventDataNameProvider<Type> publisherEventDataNameProvider,
        IJobDataStorage storage)
    {
        _publisherEventIdProvider = publisherEventIdProvider;
        _publisherEventDataNameProvider = publisherEventDataNameProvider;
        _storage = storage;
    }

    public void Set<TData>(TData data) where TData : notnull
    {
        _storage.Insert(
            jobId: _publisherEventIdProvider.Provide(),
            data: new Dictionary<string, object>()
            {
                { _publisherEventDataNameProvider.Provide(typeof(TData)), data }
            });
    }

    public TData Fetch<TData>()
    {
        return _storage.Fetch<TData>(
            jobId: _publisherEventIdProvider.Provide(),
            parameterName: _publisherEventDataNameProvider.Provide(typeof(TData)));
    }
}
