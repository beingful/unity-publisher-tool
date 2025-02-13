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

    internal override async Task<TInternalModel> GetAsync<TExternalModel, TInternalModel>(
        string endpoint, CancellationToken cancellationToken = default)
    {
        await CheckSessionAsync(cancellationToken);

        return await base.GetAsync<TExternalModel, TInternalModel>(endpoint, cancellationToken);
    }

    internal override async Task<TResponse> GetAsync<TResponse>(
        string endpoint, CancellationToken cancellationToken = default)
    {
        await CheckSessionAsync(cancellationToken);

        return await base.GetAsync<TResponse>(endpoint, cancellationToken);
    }

    private async ValueTask CheckSessionAsync(CancellationToken cancellationToken = default)
    {
        if (_sessionManager.IsSessionAlive() == false)
        {
            await _sessionManager.SetUpSessionAsync(cancellationToken);
        }
    }
}
