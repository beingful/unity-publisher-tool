namespace Unity.Publisher.Tool.Domain.Business.Publisher.Models;

public class Assets
{
    public readonly Asset[] Collection;

    public Assets(Asset[] collection)
    {
        Collection = collection;
    }
}
