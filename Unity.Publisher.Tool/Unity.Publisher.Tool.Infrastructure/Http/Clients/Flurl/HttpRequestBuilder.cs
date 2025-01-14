using Flurl.Http;
using Flurl.Http.Content;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients.Flurl;

public class HttpRequestBuilder
{
    private readonly IFlurlRequest _flurlRequest;

    public HttpRequestBuilder(IFlurlRequest flurlRequest)
    {
        _flurlRequest = flurlRequest;
    }

    public HttpRequestBuilder WithHeaders(params (string Name, string Value)[] headers)
    {
        foreach ((string name, string value) in headers)
        {
            _flurlRequest.Headers.Add(name, value);
        }

        return this;
    }

    public HttpRequestBuilder WithCookies(CookieJar cookieJar)
    {
        _flurlRequest.WithCookies(cookieJar);

        return this;
    }

    public HttpRequestBuilder WithJsonContent(object? content)
    {
        _flurlRequest.Content = new CapturedJsonContent(
            json: _flurlRequest
                .Settings
                .JsonSerializer
                .Serialize(content));

        return this;
    }

    public HttpRequestBuilder WithUrlEncodedContent(object? content)
    {
        _flurlRequest.Content = new CapturedUrlEncodedContent(
            data: _flurlRequest
                .Settings
                .UrlEncodedSerializer
                .Serialize(content));

        return this;
    }

    public HttpRequestBuilder SetMethod(HttpMethod method)
    {
        _flurlRequest.Verb = method;

        return this;
    }

    public IFlurlRequest Build()
    {
        return _flurlRequest;
    }
}
