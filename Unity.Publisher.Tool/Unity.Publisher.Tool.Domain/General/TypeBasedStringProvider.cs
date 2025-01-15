using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.General;

public class TypeBasedStringProvider : IDataNameProvider
{
    public string Provide<TData>()
    {
        return typeof(TData).Name.ToLower();
    }
}
