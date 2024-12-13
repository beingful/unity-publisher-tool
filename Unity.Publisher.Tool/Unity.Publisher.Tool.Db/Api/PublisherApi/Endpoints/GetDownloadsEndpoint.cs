namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetDownloadsEndpoint : PublisherEndpoint
{
    public GetDownloadsEndpoint(long publisherId) : base(publisherId)
    {
    }

    public override string Path()
    {
        int requestedPeriod = DateTime.Now.Year * 100 + DateTime.Now.Month;

        return WithPulisherId($"publisher-info/downloads/{{0}}/{requestedPeriod}.json?package_filter=all");
    }
}
