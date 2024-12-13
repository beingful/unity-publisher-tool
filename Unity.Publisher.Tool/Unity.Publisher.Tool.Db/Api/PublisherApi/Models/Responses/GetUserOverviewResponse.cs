using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

internal class GetUserOverviewResponse : IConvertibleTo<PublisherProfile>
{
    [JsonPropertyName("publisher_id")]
    public required long Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    public PublisherProfile Convert()
    {
        return new PublisherProfile(Id, Name);
    }
}
