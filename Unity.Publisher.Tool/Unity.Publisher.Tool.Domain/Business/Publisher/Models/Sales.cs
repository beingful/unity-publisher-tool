namespace Unity.Publisher.Tool.Domain.Business.Publisher.Models;

public class Sales
{
    public readonly Sale[] Collection;

    public Sales(Sale[] collection)
    {
        Collection = collection;
    }

    public int ItemsSold => Collection.Sum(x => x.CopiesSold);

    public decimal Revenue => Collection.Sum(x => x.Revenue);

    public bool IsEmpty => Count == 0;

    public int Count => Collection.Length;

    public Sale this[int index] => Collection[index];

    public static Sales Empty()
    {
        return new Sales(Array.Empty<Sale>());
    }
}
