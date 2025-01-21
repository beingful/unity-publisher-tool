namespace Unity.Publisher.Tool.Domain.Publisher;

public class PublisherStatement
{
    public readonly AssetStatement[] AssetsStatements;

    public readonly DateTime CreationTime;

    public PublisherStatement(AssetStatement[] assetsStatements, DateTime creationTime)
    {
        AssetsStatements = assetsStatements;
        CreationTime = creationTime;
    }

    public bool IsEmpty => AssetsStatements.Length == 0;

    public int Size => AssetsStatements.Length;

    public static PublisherStatement Empty()
    {
        return new PublisherStatement(Array.Empty<AssetStatement>(), DateTime.MinValue);
    }

    public static PublisherStatement Create(Assets assets, Sales sales,
        Reviews reviews, Downloads downloads, DateTime creationTime)
    {
        Dictionary<string, Sales> salesByAsset = sales.Collection
            .GroupBy(x => x.ProductTag.Product)
            .ToDictionary(x => x.Key, x => new Sales(x.ToArray()));

        Dictionary<string, Reviews> reviewsByAsset = reviews.Collection
            .GroupBy(x => x.Product)
            .ToDictionary(x => x.Key, x => new Reviews(x.ToArray()));

        Dictionary<string, Download> downloadsByAsset = downloads.Collection
            .ToDictionary(x => x.Product);

        AssetStatement[] assetStatements = CreateAssetStatements(
            assets, salesByAsset, reviewsByAsset, downloadsByAsset);

        return new PublisherStatement(assetStatements, creationTime);
    }

    private static AssetStatement[] CreateAssetStatements(
        Assets assets,
        Dictionary<string, Sales> sales,
        Dictionary<string, Reviews> reviews,
        Dictionary<string, Download> downloads)
    {
        List<AssetStatement> assetEvents = [];

        foreach (Asset asset in assets.Collection.OrderBy(x => x.Id))
        {
            bool assetRelatedInfoIsProvided =
                sales.TryGetValue(asset.Name, out Sales? assetSales)
                | reviews.TryGetValue(asset.Name, out Reviews? assetReviews)
                | downloads.TryGetValue(asset.Name, out Download? assetDownloads);

            if (assetRelatedInfoIsProvided)
            {
                assetEvents.Add(new AssetStatement(
                asset: asset,
                sales: assetSales ?? Sales.Empty(),
                reviews: assetReviews ?? Reviews.Empty(),
                downloads: assetDownloads ?? Download.Empty(asset.Name)));
            }
        }

        return [.. assetEvents];
    }
}
