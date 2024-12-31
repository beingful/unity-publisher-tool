namespace Unity.Publisher.Tool.App.Services;

public class TypeNameProvider : IPublisherEventDataNameProvider<Type>
{
    public string Provide(Type type)
    {
        return type.Name.ToLower();
    }
}
