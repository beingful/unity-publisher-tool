namespace Unity.Publisher.Tool.Domain.Storage;

public interface IKeyedDataStorage
{
    void Insert(string key, Dictionary<string, object> data);

    TData? Fetch<TData>(string key, string name) where TData : class;
}
