namespace Unity.Publisher.Tool.Domain.Publisher;

public class Reviews
{
    public readonly Review[] Collection;

    public Reviews(Review[] collection)
    {
        Collection = collection;
    }

    public bool IsEmpty => Count == 0;

    public int Count => Collection.Length;

    public Review this[int index] => Collection[index];

    public static Reviews Empty()
    {
        return new Reviews(Array.Empty<Review>());
    }
}
