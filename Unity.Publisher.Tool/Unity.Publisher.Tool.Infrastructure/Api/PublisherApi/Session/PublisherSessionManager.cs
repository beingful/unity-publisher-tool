using System.Net;
using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Infrastructure.Api.State;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Session;

public class PublisherSessionManager : ISessionManager
{
    private const string _sessionIdCookieName = "kharma_session";

    private readonly IHttpClient _httpClient;
    private readonly ILogInManager _logInManager;
    private readonly ILogger _logger;

    public PublisherSessionManager(
        IHttpClient<PublisherApi> httpClient,
        ILogInManager<PublisherApi> logInManager,
        ILogger<PublisherSessionManager> logger)
    {
        _httpClient = httpClient;
        _logInManager = logInManager;
        _logger = logger;
    }

    public bool IsSessionAlive()
    {
        Cookie? cookie = _httpClient.GetCookie(_sessionIdCookieName);

        bool isSessionAlive =
            string.IsNullOrWhiteSpace(cookie?.Value) == false
            && DateTime.Now.AddMinutes(10) <= cookie?.Expires.ToLocalTime();

        if (!isSessionAlive)
        {
            _logger.LogInformation("The session is not alive.");
        }

        return string.IsNullOrWhiteSpace(cookie?.Value) == false && cookie?.Expired == false;
    }

    public async Task SetUpSessionAsync(CancellationToken cancellationToken = default)
    {
        using (_logger.BeginScope("Start a log-in process."))
        {
            try
            {
                await _logInManager.LogInAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The error occured during the log-in process.");
            }

            _logger.LogInformation("The log-in process is done.");
        }
    }
}
