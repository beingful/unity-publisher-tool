namespace Unity.Publisher.Tool.Domain.Publisher;

public sealed class Asset
{
    public readonly long Id;

    public readonly string Name;

    public readonly int Rating;

    public Asset(long id, string name, int rating)
    {
        Id = id;
        Name = name;
        Rating = rating;
    }
}
