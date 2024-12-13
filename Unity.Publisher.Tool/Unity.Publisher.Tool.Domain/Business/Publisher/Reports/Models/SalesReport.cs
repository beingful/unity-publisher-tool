using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

public class SalesReport
{
    public readonly Sales Sales;

    public SalesReport(Sales sales)
    {
        Sales = sales;
    }
}
