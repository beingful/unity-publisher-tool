namespace Unity.Publisher.Tool.Domain.Data;

public class KeyedProvider<TKey, TValue> : IKeyedProvider<TKey, TValue>
    where TKey : notnull
{
    public readonly IReadOnlyDictionary<TKey, TValue> _components;

    public KeyedProvider(Dictionary<TKey, TValue> components)
    {
        _components = components;
    }

    public TValue Provide(TKey key)
    {
        return _components[key];
    }
}
