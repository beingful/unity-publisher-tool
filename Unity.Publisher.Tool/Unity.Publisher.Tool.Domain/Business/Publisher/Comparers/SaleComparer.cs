using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Comparers;

public class SaleComparer : IDataComparer<Sale>
{
    public bool Different(Sale first, Sale second)
    {
        return first.Revenue != second.Revenue;
    }

    public Sale Difference(Sale left, Sale right)
    {
        return new Sale(
            productTag: left.ProductTag,
            copiesSold: Math.Max(left.CopiesSold - right.CopiesSold, 0),
            revenue: Math.Max(left.Revenue - right.Revenue, 0));
    }
}
