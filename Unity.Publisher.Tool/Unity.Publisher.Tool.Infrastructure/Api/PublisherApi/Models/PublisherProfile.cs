namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

internal sealed class PublisherProfile
{
    public readonly long Id;

    public readonly string Name;

    public PublisherProfile(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public static PublisherProfile Empty()
    {
        return new PublisherProfile(0, string.Empty);
    }
}
