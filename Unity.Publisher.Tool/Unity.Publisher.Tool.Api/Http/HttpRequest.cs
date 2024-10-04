using Flurl.Http;
using Flurl.Http.Newtonsoft;
using Newtonsoft.Json;

namespace Unity.Publisher.Tool.Http;

public class HttpRequest
{
    private readonly IFlurlRequest _flurlRequest;

    public HttpRequest(IFlurlRequest flurlRequest, JsonSerializerSettings? serializerSettings = null)
    {
        _flurlRequest = flurlRequest;

        if (serializerSettings != null)
        {
            _flurlRequest.WithSettings(settings =>
            {
                settings.JsonSerializer = new NewtonsoftJsonSerializer(serializerSettings);
            });
        }
    }

    public HttpRequest AddHeaders(params (string Name, string Value)[] headers)
    {
        foreach ((string name, string value) in headers)
        {
            _flurlRequest.Headers.Add(name, value);
        }

        return this;
    }

    public async Task PostWithContentAsync(object request)
    {
        await _flurlRequest.PostJsonAsync(request);
    }
}
