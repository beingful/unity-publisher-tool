using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;

namespace Unity.Publisher.Tool.Infrastructure.Api.State;

public abstract class StatefulApi : ExternalApi
{
    private readonly ISessionManager _sessionManager;

    public StatefulApi(
        IHttpClient<StatefulApi> httpClient,
        ISessionManager<StatefulApi> sessionManager,
        ILogger<StatefulApi> logger) : base(httpClient, logger)
    {
        _sessionManager = sessionManager;
    }

    protected override async Task<TInternalModel> GetAsync<TExternalModel, TInternalModel>(string endpoint)
    {
        await CheckSessionAsync();

        return await base.GetAsync<TExternalModel, TInternalModel>(endpoint);
    }

    protected override async Task<TResponse> GetAsync<TResponse>(string endpoint)
    {
        await CheckSessionAsync();

        return await base.GetAsync<TResponse>(endpoint);
    }

    private async ValueTask CheckSessionAsync()
    {
        if (_sessionManager.IsSessionAlive() == false)
        {
            await _sessionManager.SetUpSessionAsync();
        }
    }
}
