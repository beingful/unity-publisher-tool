using Flurl.Http;

namespace Unity.Publisher.Tool.Infrastructure.Http.Responses.Flurl;

public class HttpResponse : IHttpResponse
{
    private readonly IFlurlResponse _response;

    public HttpResponse(IFlurlResponse response)
    {
        _response = response;
    }

    public string? RequestUrl()
    {
        return _response.ResponseMessage.RequestMessage?.RequestUri?.ToString();
    }

    public void Dispose()
    {
        _response.Dispose();
    }
}
