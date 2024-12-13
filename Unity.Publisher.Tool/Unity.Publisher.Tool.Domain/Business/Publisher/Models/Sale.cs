namespace Unity.Publisher.Tool.Domain.Business.Publisher.Models;

public sealed class Sale
{
    public readonly ProductTag ProductTag;
    public readonly int CopiesSold;
    public readonly decimal Revenue;

    public Sale(ProductTag productTag, int copiesSold, decimal revenue)
    {
        ProductTag = productTag;
        CopiesSold = copiesSold;
        Revenue = revenue;
    }

    public bool IsEmpty => CopiesSold == 0;

    public static Sale Create(ProductTag productTag,
        int copiesHanded, Loss loss, decimal revenue)
    {
        return new Sale(
            productTag: productTag,
            copiesSold: copiesHanded - loss.Total,
            revenue: revenue);
    }

    public static Sale Empty(string product)
    {
        return new Sale(
            productTag: new ProductTag(product: product, price: 0),
            copiesSold: 0,
            revenue: 0);
    }
}
