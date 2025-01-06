namespace Unity.Publisher.Tool.Domain.Storage;

public class DataStorage : IDataStorage
{
    private readonly IStorageKeyProvider _storageKeyProvider;
    private readonly IDataNameProvider _dataNameProvider;
    private readonly IKeyedDataStorage _dataStorage;

    public DataStorage(
        IStorageKeyProvider storageKeyProvider,
        IDataNameProvider dataNameProvider,
        IKeyedDataStorage dataStorage)
    {
        _storageKeyProvider = storageKeyProvider;
        _dataNameProvider = dataNameProvider;
        _dataStorage = dataStorage;
    }

    public void Set<TData>(TData data) where TData : class
    {
        _dataStorage.Insert(
            key: _storageKeyProvider.Provide(),
            data: new Dictionary<string, object>()
            {
                { _dataNameProvider.Provide<TData>(), data }
            });
    }

    public TData? Fetch<TData>() where TData : class
    {
        return _dataStorage.Fetch<TData>(
            key: _storageKeyProvider.Provide(),
            name: _dataNameProvider.Provide<TData>());
    }
}
