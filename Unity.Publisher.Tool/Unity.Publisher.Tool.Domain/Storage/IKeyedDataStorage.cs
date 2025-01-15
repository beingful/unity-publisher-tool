namespace Unity.Publisher.Tool.Domain.Storage;

public interface IKeyedDataStorage
{
    void Insert(string key, KeyValuePair<string, object> parameter);

    TData? Fetch<TData>(string key, string name) where TData : class;
}
