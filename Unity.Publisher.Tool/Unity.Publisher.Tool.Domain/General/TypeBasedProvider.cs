namespace Unity.Publisher.Tool.Domain.General;

public class TypeBasedProvider<TComponent> : KeyedProvider<Type, TComponent>
{
    public TypeBasedProvider(Dictionary<Type, TComponent> components) : base(components)
    {
    }
}

