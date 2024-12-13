using Microsoft.Extensions.Options;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Actions;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Models;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Options;
using Unity.Publisher.Tool.Infrastructure.Api.State;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn;

public sealed class PublisherLogInManager : ILogInManager
{
    private readonly LogInAction _logInAction;
    private readonly UserCredentialsAction _userCredentialsAction;
    private readonly PublisherPortalAction _publisherPortalAction;
    private readonly CallbackAction _callBackAction;

    public PublisherLogInManager(
        IHttpClient<PublisherLogInManager> httpClient,
        IOptions<PublisherAccountOptions> accountOptions)
    {
        _logInAction = new LogInAction(httpClient);
        _userCredentialsAction = new UserCredentialsAction(httpClient, accountOptions);
        _publisherPortalAction = new PublisherPortalAction(httpClient);
        _callBackAction = new CallbackAction(httpClient);
    }

    public async Task LogInAsync()
    {
        LogInDataResult logInDataResult = await _logInAction.GetPersonalizedDataAsync();

        CallbackPageResult authCallbackPageResult = await _userCredentialsAction.SendAsync(logInDataResult);

        await _callBackAction.RedirectAsync(authCallbackPageResult);

        CallbackPageResult bounceCallbackPageResult = await _publisherPortalAction.RequestPageAsync();

        await _callBackAction.RedirectAsync(bounceCallbackPageResult);
    }
}
