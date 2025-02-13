using Autofac;
using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;
using Unity.Publisher.Tool.Infrastructure.Api.State;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public sealed class PublisherApi : StatefulApi, IStartable
{
    private static PublisherProfile _publisher = PublisherProfile.Empty();

    public PublisherApi(
        IHttpClient<PublisherApi> httpClient,
        ISessionManager<PublisherApi> sessionManager,
        ILogger<PublisherApi> logger) : base(httpClient, sessionManager, logger)
    {
    }

    internal PublisherProfile Publisher => _publisher;

    void IStartable.Start()
    {
        _publisher = GetPublisherProfileAsync().Result;
    }

    private Task<PublisherProfile> GetPublisherProfileAsync()
    {
        string endpoint = PublisherApiEndpoints.PublisherProfile();

        return GetAsync<GetUserOverviewResponse, PublisherProfile>(endpoint);
    }
}
