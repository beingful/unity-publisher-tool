namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetDownloadsEndpoint : PublisherEndpoint
{
    private readonly DateTime _requestTimestamp;

    public GetDownloadsEndpoint(long publisherId, DateTime requestTimestamp) : base(publisherId)
    {
        _requestTimestamp = requestTimestamp;
    }

    public override string Path()
    {
        int requestedPeriod = _requestTimestamp.Year * 100 + _requestTimestamp.Month;

        return WithPulisherId($"publisher-info/downloads/{{0}}/{requestedPeriod}.json?package_filter=all");
    }
}
