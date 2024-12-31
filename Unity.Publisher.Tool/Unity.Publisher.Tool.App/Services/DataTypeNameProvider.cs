using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public class DataTypeNameProvider : IProvider<Type, string>
{
    public string Provide(Type data)
    {
        return data.Name.ToLower();
    }
}
