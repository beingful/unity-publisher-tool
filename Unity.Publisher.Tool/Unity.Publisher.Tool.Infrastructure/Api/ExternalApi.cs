using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api;

public abstract class ExternalApi
{
    private readonly IHttpClient _httpClient;
    private readonly ILogger _logger;

    public ExternalApi(IHttpClient<ExternalApi> httpClient, ILogger<ExternalApi> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    internal virtual async Task<TInternalModel> GetAsync<TExternalModel, TInternalModel>(string endpoint, CancellationToken cancellationToken = default)
        where TExternalModel : IConvertible<TInternalModel>
    {
        TExternalModel response = await GetAsync<TExternalModel>(endpoint, cancellationToken);

        return response.Convert();
    }

    internal virtual async Task<TResponse> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default)
    {
        try
        {
            using IJsonHttpResponse jsonResponse = await _httpClient
                .GetAsync<IJsonHttpResponse>(endpoint, cancellationToken);

            return await jsonResponse.ReadAs<TResponse>();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"The error occured during GET {endpoint} request.");

            throw;
        }
    }
}
