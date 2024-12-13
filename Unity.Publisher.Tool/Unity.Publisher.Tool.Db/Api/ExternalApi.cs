using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api;

public abstract class ExternalApi
{
    protected readonly ILogger Logger;

    private readonly IHttpClient _httpClient;

    public ExternalApi(IHttpClient<ExternalApi> httpClient, ILogger<ExternalApi> logger)
    {
        _httpClient = httpClient;
        Logger = logger;
    }

    protected virtual async Task<TInternalModel> GetAsync<TExternalModel, TInternalModel>(string endpoint)
        where TExternalModel : IConvertibleTo<TInternalModel>
    {
        TExternalModel response = await GetAsync<TExternalModel>(endpoint);

        return response.Convert();
    }

    protected virtual async Task<TResponse> GetAsync<TResponse>(string endpoint)
    {
        try
        {
            using IJsonHttpResponse jsonResponse = await _httpClient
                .GetAsync<IJsonHttpResponse>(endpoint);

            return await jsonResponse.ReadAs<TResponse>();
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, $"The error occured during GET {endpoint} request.");

            throw;
        }
    }
}
