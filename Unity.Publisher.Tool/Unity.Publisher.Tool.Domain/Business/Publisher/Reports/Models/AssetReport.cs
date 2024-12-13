using Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

public sealed class AssetReport
{
    public readonly Asset Asset;

    public readonly Sales Sales;

    public readonly Reviews Reviews;

    public readonly Download Downloads;

    public AssetReport(Asset asset, Sales sales, Reviews reviews, Download downloads)
    {
        Asset = asset;
        Sales = sales;
        Reviews = reviews;
        Downloads = downloads;
    }

    public static AssetReport FromStatement(AssetStatement statement)
    {
        return new AssetReport(statement.Asset, statement.Sales, statement.Reviews, statement.Downloads);
    }
}
