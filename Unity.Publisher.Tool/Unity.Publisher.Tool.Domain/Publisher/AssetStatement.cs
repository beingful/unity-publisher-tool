namespace Unity.Publisher.Tool.Domain.Publisher;

public class AssetStatement
{
    public readonly Asset Asset;

    public  Sales Sales;

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
