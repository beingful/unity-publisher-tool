using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherEventIdProvider : IProvider<string>
{
    private readonly PublisherEvent _seed;

    public PublisherEventIdProvider(PublisherEvent seed)
    {
        _seed = seed;
    }

    public string Provide()
    {
        return Enum.GetName(_seed)!.ToUpper();
    }
}
