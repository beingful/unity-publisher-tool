//using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;
//using Unity.Publisher.Tool.Domain.Data;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Comparers;

//public class SalesReportComparer : IDataComparer<SalesReport>
//{
//    public bool Different(SalesReport first, SalesReport second)
//    {
//        return first.ItemsSold != second.ItemsSold;
//    }

//    public SalesReport Difference(SalesReport left, SalesReport right)
//    {
//        List<Sale> difference = [];

//        for (int i = 0; i < right.Sales.Length; ++i)
//        {
//            if (left.Sales[i].CopiesSold > right.Sales[i].CopiesSold)
//            {
//                difference.Add(
//                    item: Difference(left.Sales[i], right.Sales[i]));
//            }
//        }

//        for (int i = right.Sales.Length; i < left.Sales.Length; ++i)
//        {
//            difference.Add(item: left.Sales[i]);
//        }

//        return new SalesReport(sales: [.. difference]);
//    }

//    private Sale Difference(Sale currentSale, Sale previousSale)
//    {
//        return new Sale(
//            product: currentSale.Product,
//            price: currentSale.Price,
//            copiesHanded: currentSale.CopiesHanded - previousSale.CopiesHanded,
//            loss: new Loss(
//                refund: currentSale.Loss.Refund - previousSale.Loss.Refund,
//                chargeback: currentSale.Loss.Chargeback - previousSale.Loss.Chargeback),
//            revenue: currentSale.CopiesSold == 0
//                ? 0
//                : currentSale.Revenue / currentSale.CopiesSold
//                    * (currentSale.CopiesSold - previousSale.CopiesSold));
//    }
//}
