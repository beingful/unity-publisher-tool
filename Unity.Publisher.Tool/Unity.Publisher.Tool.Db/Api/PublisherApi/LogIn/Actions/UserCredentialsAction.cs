using Microsoft.Extensions.Options;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Models;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Options;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Actions;

public sealed class UserCredentialsAction
{
    private readonly IHttpClient _httpClient;
    private readonly PublisherAccountOptions _accountOptions;

    public UserCredentialsAction(
        IHttpClient httpClient,
        IOptions<PublisherAccountOptions> accountOptions)
    {
        _httpClient = httpClient;
        _accountOptions = accountOptions.Value;
    }

    public async Task<CallbackPageResult> SendAsync(LogInDataResult logInData, CancellationToken cancellationToken = default)
    {
        IHtmlHttpResponse authCallbackResponse = await SendCredentialsAsync(logInData, cancellationToken);

        return new CallbackPageResult(HtmlContent: authCallbackResponse.Content);
    }

    private Task<IHtmlHttpResponse> SendCredentialsAsync(LogInDataResult logInData, CancellationToken cancellationToken)
    {
        return _httpClient.PostUrlEncodedAsync<IHtmlHttpResponse>(
            content: new Dictionary<string, string>
            {
                { "utf8", "&#x2713;" },
                { "_method", "put" },
                { "authenticity_token", logInData.AuthToken },
                { "conversations_create_session_form[email]", _accountOptions.Email },
                { "conversations_create_session_form[password]", _accountOptions.Password },
                { "conversations_create_session_form[remember_me]", "true" },
                { "commit", "Sign in" }
            },
            endpoint: logInData.Url);
    }
}
