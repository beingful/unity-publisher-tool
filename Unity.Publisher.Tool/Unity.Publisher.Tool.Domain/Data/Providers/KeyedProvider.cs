namespace Unity.Publisher.Tool.Domain.Data.Providers;

public class KeyedProvider<TComponent> : IKeyedProvider<Type, TComponent>
{
    public readonly IReadOnlyDictionary<Type, TComponent> _components;

    public KeyedProvider(Dictionary<Type, TComponent> components)
    {
        _components = components;
    }

    public TComponent Provide(Type key)
    {
        return _components[key];
    }
}

