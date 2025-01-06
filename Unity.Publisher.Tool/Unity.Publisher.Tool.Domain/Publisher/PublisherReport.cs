namespace Unity.Publisher.Tool.Domain.Publisher;

public sealed class PublisherReport
{
    public readonly PublisherInfo Publisher;

    public readonly PublisherStatement Statement;

    public readonly Revenue Revenue;

    public readonly Month Month;

    public PublisherReport(PublisherInfo publisher, PublisherStatement statement, Revenue revenue, Month month)
    {
        Publisher = publisher;
        Statement = statement;
        Revenue = revenue;
        Month = month;
    }

    public bool IsEmpty => Statement.IsEmpty;

    public static PublisherReport Empty(Month month)
    {
        return new PublisherReport(
            publisher: PublisherInfo.Empty(),
            statement: PublisherStatement.Empty(),
            revenue: Revenue.Zero(),
            month: month);
    }
}
