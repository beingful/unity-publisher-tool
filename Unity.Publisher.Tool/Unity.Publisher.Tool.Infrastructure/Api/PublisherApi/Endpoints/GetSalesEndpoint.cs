namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetSalesEndpoint : PublisherEndpoint
{
    private readonly DateTime _requestTimestamp;

    public GetSalesEndpoint(long publisherId, DateTime requestTimestamp) : base(publisherId)
    {
        _requestTimestamp = requestTimestamp;
    }

    public override string Path()
    {
        int requestedPeriod = _requestTimestamp.Year * 100 + _requestTimestamp.Month;

        return WithPulisherId($"publisher-info/sales/{{0}}/{requestedPeriod}.json");
    }
}
