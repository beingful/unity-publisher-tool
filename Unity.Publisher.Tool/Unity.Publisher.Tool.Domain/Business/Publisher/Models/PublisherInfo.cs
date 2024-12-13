namespace Unity.Publisher.Tool.Domain.Business.Publisher.Models;

public sealed class PublisherInfo
{
    public readonly long Id;

    public readonly string Name;

    public readonly Rating Rating;

    public PublisherInfo(long id, string name, Rating rating)
    {
        Id = id;
        Name = name;
        Rating = rating;
    }

    public static PublisherInfo Empty()
    {
        return new PublisherInfo(id: 0, name: string.Empty, rating: Rating.Zero());
    }
}
