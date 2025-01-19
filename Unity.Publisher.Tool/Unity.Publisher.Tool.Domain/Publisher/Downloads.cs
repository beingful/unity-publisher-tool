namespace Unity.Publisher.Tool.Domain.Publisher;

public class Downloads
{
    public readonly Download[] Collection;

    public Downloads(Download[] collection)
    {
        Collection = collection;
    }

    public static Downloads Empty()
    {
        return new Downloads(collection: Array.Empty<Download>());
    }
}
