using Unity.Publisher.Tool.Domain.Publisher;

namespace Unity.Publisher.Tool.Domain.Test.Comparers;

public partial class SaleComparerTests
{
    public static IEnumerable<object[]> EqualSales()
    {
        Sale sale = new(
            productTag: new ProductTag(product: "Product", price: 1),
            copiesSold: 5,
            revenue: 5);

        Sale sameSaleWithRefund = Sale.Create(
            productTag: new ProductTag(product: "Product", price: 1),
            copiesHanded: 6,
            loss: new Loss(refund: 1, chargeback: 0),
            revenue: 5);

        Sale sameSaleWithChargeback = Sale.Create(
            productTag: new ProductTag(product: "Product", price: 1),
            copiesHanded: 6,
            loss: new Loss(refund: 0, chargeback: 1),
            revenue: 5);

        Sale emptySale = Sale.Empty("Product");

        return [
            [ sale, sale ],
            [ sale, sameSaleWithRefund ],
            [ sale, sameSaleWithChargeback ],
            [ emptySale, emptySale ]
        ];
    }

    public static IEnumerable<object[]> DifferentSales()
    {
        return [..DifferentSalesBiggerGoFirst(), ..DifferentSalesBiggerGoSecond()];
    }

    public static IEnumerable<object[]> DifferentSalesBiggerGoFirst()
    {
        return DifferentSalesBiggerGoSecond()
            .Select(x => new object[] { x[1], x[0] });
    }

    public static IEnumerable<object[]> DifferentSalesBiggerGoSecond()
    {
        ProductTag productTag = new(product: "Product", price: 1);

        Sale sale = new(
            productTag: productTag,
            copiesSold: 4,
            revenue: 4);

        Sale oneMoreCopySold = new(
            productTag: productTag,
            copiesSold: 5,
            revenue: 5);

        Sale oneCopyRefunded = Sale.Create(
            productTag: new ProductTag(product: "Product", price: 1),
            copiesHanded: 4,
            loss: new Loss(refund: 1, chargeback: 0),
            revenue: 3);

        Sale oneCopyChargebacked = Sale.Create(
            productTag: new ProductTag(product: "Product", price: 1),
            copiesHanded: 4,
            loss: new Loss(refund: 0, chargeback: 1),
            revenue: 3);

        Sale emptySale = Sale.Empty("Product");

        return [
            [ sale, oneMoreCopySold ],
            [ oneCopyRefunded, sale ],
            [ oneCopyChargebacked, sale ],
            [ emptySale, sale ]
        ];
    }

    public static IEnumerable<object[]> DifferentSalesWithEmptyDifference()
    {
        return [.. EqualSales(), .. DifferentSalesBiggerGoSecond()];
    }

    public static IEnumerable<object[]> DifferentSalesWithNotEmptyDifference()
    {
        ProductTag productTag = new ProductTag(product: "Product", price: 1);

        Sale sale = new(
            productTag: productTag,
            copiesSold: 5,
            revenue: 5);

        Sale oneMoreCopySold = new(
            productTag: productTag,
            copiesSold: 6,
            revenue: 6);

        Sale saleWithOtherRevenueDifference = new(
            productTag: productTag,
            copiesSold: 1,
            revenue: 1);

        Sale saleWithRefund = Sale.Create(
            productTag: new ProductTag(product: "Product", price: 1),
            copiesHanded: 5,
            loss: new Loss(refund: 1, chargeback: 0),
            revenue: 4);

        Sale saleWithChargeback = Sale.Create(
            productTag: new ProductTag(product: "Product", price: 1),
            copiesHanded: 5,
            loss: new Loss(refund: 0, chargeback: 1),
            revenue: 4);

        Sale emptySale = Sale.Empty("Product");

        return [
            [ oneMoreCopySold, sale,  ],
            [ saleWithRefund, sale ],
            [ saleWithChargeback, sale ],
            [ emptySale, sale ]
        ];
    }
}
