using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Comparers;

public class SalesComparer : IDataComparer<Sales>
{
    private readonly IDataComparer<Sale> _saleComparer;

    public SalesComparer(IDataComparer<Sale> saleComparer)
    {
        _saleComparer = saleComparer;
    }

    public bool Different(Sales first, Sales second)
    {
        return first.ItemsSold != second.ItemsSold;
    }

    public Sales Difference(Sales left, Sales right)
    {
        List<Sale> differences = [];

        for (int i = 0, j = 0; i < left.Count; ++i)
        {
            Sale newSale = left[i];
            Sale? oldSale = right.Collection.ElementAtOrDefault(i);

            if (newSale.ProductTag.Equals(oldSale?.ProductTag))
            {
                if (_saleComparer.Different(newSale, oldSale))
                {
                    differences.Add(_saleComparer.Difference(newSale, oldSale));
                }

                ++j;
            }
            else
            {
                differences.Add(newSale);
            }
        }

        return new Sales(differences.ToArray());
    }
}
