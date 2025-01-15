using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.General;

public class EnumBasedStringProvider<TEnum> : IPublisherEventIdProvider, IStorageKeyProvider
    where TEnum : notnull
{
    private readonly TEnum _seed;

    public EnumBasedStringProvider(TEnum seed)
    {
        _seed = seed;
    }

    public string Provide()
    {
        return Enum.GetName(typeof(TEnum), _seed)!.ToLower();
    }
}
