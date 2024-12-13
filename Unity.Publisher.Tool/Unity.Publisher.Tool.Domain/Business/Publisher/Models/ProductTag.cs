namespace Unity.Publisher.Tool.Domain.Business.Publisher.Models;

public class ProductTag : IEquatable<ProductTag>
{
    public readonly string Product;
    public readonly decimal Price;

    public ProductTag(string product, decimal price)
    {
        Product = product;
        Price = price;
    }

    public bool Equals(ProductTag? other)
    {
        return Product == other?.Product && Price == other?.Price;
    }
}
