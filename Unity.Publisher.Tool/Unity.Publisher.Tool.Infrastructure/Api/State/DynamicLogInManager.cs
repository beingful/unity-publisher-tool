using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Infrastructure.Api.State;

public class DynamicLogInManager<TExternalApi> : ILogInManager<TExternalApi>
    where TExternalApi : ExternalApi
{
    private readonly ILogInManager _logInManager;

    public DynamicLogInManager(IKeyedProvider<Type, ILogInManager> logInManagerProvider)
    {
        _logInManager = logInManagerProvider.Provide(typeof(TExternalApi));
    }

    public Task LogInAsync(CancellationToken cancellationToken = default)
    {
        return _logInManager.LogInAsync(cancellationToken);
    }
}
