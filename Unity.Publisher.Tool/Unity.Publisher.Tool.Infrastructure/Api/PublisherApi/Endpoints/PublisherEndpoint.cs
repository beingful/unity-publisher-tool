namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal abstract class PublisherEndpoint : ApiEndpoint
{
    private readonly long _publisherId;

    public PublisherEndpoint(long publisherId)
    {
        _publisherId = publisherId;
    }

    protected string WithPulisherId(string path)
    {
        return string.Format(path, _publisherId);
    }
}
