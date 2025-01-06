namespace Unity.Publisher.Tool.Domain.Publisher;

public class Downloads
{
    public readonly Download[] Collection;

    public Downloads(Download[] collection)
    {
        Collection = collection;
    }
}
