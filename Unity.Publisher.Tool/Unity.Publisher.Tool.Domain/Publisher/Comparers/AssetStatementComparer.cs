using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Comparers;

public class AssetStatementComparer : IDataComparer<AssetStatement>
{
    private readonly IDataComparer<Sales> _salesComparer;
    private readonly IDataComparer<Reviews> _reviewsComparer;
    private readonly IDataComparer<Download> _downloadsComparer;

    public AssetStatementComparer(
        IDataComparer<Sales> salesComparer,
        IDataComparer<Reviews> reviewsComparer,
        IDataComparer<Download> downloadsComparer)
    {
        _salesComparer = salesComparer;
        _reviewsComparer = reviewsComparer;
        _downloadsComparer = downloadsComparer;
    }

    public bool Different(AssetStatement first, AssetStatement second)
    {
        return _salesComparer.Different(first.Sales, second.Sales)
            || _reviewsComparer.Different(first.Reviews, second.Reviews)
            || _downloadsComparer.Different(first.Downloads, second.Downloads);
    }

    public AssetStatement Difference(AssetStatement left, AssetStatement right)
    {
        return new AssetStatement(
            asset: left.Asset,
            sales: GetDifferenceOrDefault(left.Sales, right.Sales, _salesComparer)
                ?? Sales.Empty(),
            reviews: GetDifferenceOrDefault(left.Reviews, right.Reviews, _reviewsComparer)
                ?? Reviews.Empty(),
            downloads: GetDifferenceOrDefault(left.Downloads, right.Downloads, _downloadsComparer)
                ?? Download.Empty(left.Asset.Name));
    }

    private TData? GetDifferenceOrDefault<TData>(TData left, TData right, IDataComparer<TData> comparer)
        where TData : class
    {
        return comparer.Different(left, right)
            ? comparer.Difference(left, right)
            : null;
    }
}
