using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;

public class AssetStatement
{
    public readonly Asset Asset;

    public readonly Sales Sales;

    public readonly Reviews Reviews;

    public readonly Download Downloads;

    public AssetStatement(Asset asset, Sales sales, Reviews reviews, Download downloads)
    {
        Asset = asset;
        Sales = sales;
        Reviews = reviews;
        Downloads = downloads;
    }
}
