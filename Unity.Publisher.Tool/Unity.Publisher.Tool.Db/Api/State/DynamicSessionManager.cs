using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Infrastructure.Api.State;

public class DynamicSessionManager<TExternalApi> : ISessionManager<TExternalApi>
    where TExternalApi : ExternalApi
{
    private readonly ISessionManager _sessionManager;

    public DynamicSessionManager(IKeyedProvider<Type, ISessionManager> sessionManagerProvider)
    {
        _sessionManager = sessionManagerProvider.Provide(typeof(TExternalApi));
    }

    public bool IsSessionAlive()
    {
        return _sessionManager.IsSessionAlive();
    }

    public async Task SetUpSessionAsync()
    {
        await _sessionManager.SetUpSessionAsync();
    }
}
