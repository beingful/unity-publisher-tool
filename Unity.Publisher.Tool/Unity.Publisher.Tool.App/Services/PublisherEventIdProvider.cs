using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherEventIdProvider : IPublisherEventIdProvider
{
    private readonly PublisherEvent _seed;

    public PublisherEventIdProvider(PublisherEvent seed)
    {
        _seed = seed;
    }

    public string Provide()
    {
        return Enum.GetName(_seed)!.ToLower();
    }
}
