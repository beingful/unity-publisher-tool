using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Models;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Actions;

public sealed class LogInAction
{
    private readonly IHttpClient _httpClient;
    private readonly GetLogInPageEndpoint _logInEndpoint;

    private const string _authTokenName = "authenticity_token";

    public LogInAction(IHttpClient httpClient)
    {
        _httpClient = httpClient;
        _logInEndpoint = new GetLogInPageEndpoint();
    }

    public async Task<LogInDataResult> GetPersonalizedDataAsync(CancellationToken cancellationToken = default)
    {
        using IHtmlHttpResponse loginHtmlPageResponse = await _httpClient
            .GetAsync<IHtmlHttpResponse>(_logInEndpoint.Path(), cancellationToken);

        string? authToken = FetchAuthenticityToken(loginHtmlPageResponse);

        if (string.IsNullOrWhiteSpace(authToken))
        {
            throw new MissingMemberException($"{_authTokenName} is missing.");
        }

        return new LogInDataResult(
            AuthToken: authToken,
            Url: loginHtmlPageResponse.RequestUrl()!);
    }

    private string? FetchAuthenticityToken(IHtmlHttpResponse loginPageHtmlResponse)
    {
        return loginPageHtmlResponse
            .Content
            .SearchDocument()
            .FilterBy()
            .Attribute("name", _authTokenName)
            .BuildFilter()
            .FetchAttributeValue(targetAttribute: "value");
    }
}
