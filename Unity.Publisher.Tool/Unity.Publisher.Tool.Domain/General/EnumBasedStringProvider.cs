using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.General;

public class EnumBasedStringProvider<TEnum> : IPublisherEventIdProvider, IStorageKeyProvider
    where TEnum : notnull
{
    private readonly TEnum _data;

    public EnumBasedStringProvider(TEnum data)
    {
        _data = data;
    }

    public string Provide()
    {
        return Enum.GetName(typeof(TEnum), _data)!.ToLower();
    }
}
