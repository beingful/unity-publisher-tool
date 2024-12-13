namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetSalesEndpoint : PublisherEndpoint
{
    public GetSalesEndpoint(long publisherId) : base(publisherId)
    {
    }

    public override string Path()
    {
        int requestedPeriod = DateTime.Now.Year * 100 + DateTime.Now.Month;

        return WithPulisherId($"publisher-info/sales/{{0}}/{requestedPeriod}.json");
    }
}
