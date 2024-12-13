using System.Globalization;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Models;

public class Month
{
    public readonly int Order;

    public readonly string Name;

    public Month(int order)
    {
        Order = order;
        Name = new DateTimeFormatInfo().GetMonthName(order);
    }

    public Month Previous()
    {
        int outOfBoundFactor = (Order - 1) == 0 ? 1 : 0;

        return new Month((Order - 1) + outOfBoundFactor * 12);
    }
}
