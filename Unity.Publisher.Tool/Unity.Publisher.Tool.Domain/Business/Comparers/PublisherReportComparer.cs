using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.Domain.Business.Comparers;

public class PublisherReportComparer : IDataComparer<PublisherReport>
{
    private readonly IDataComparer<PublisherStatement> _statementComparer;

    public PublisherReportComparer(IDataComparer<PublisherStatement> statementComparer)
    {
        _statementComparer = statementComparer;
    }

    public bool Different(PublisherReport first, PublisherReport second)
    {
        return first.Month.Order == second.Month.Order
            && (first.Revenue.ForPeriod != second.Revenue.ForPeriod
                || _statementComparer.Different(first.Statement, second.Statement));
    }

    public PublisherReport Difference(PublisherReport left, PublisherReport right)
    {
        return new PublisherReport(
            publisher: left.Publisher,
            statement: _statementComparer.Difference(left.Statement, right.Statement),
            revenue: left.Revenue,
            month: left.Month);
    }
}
