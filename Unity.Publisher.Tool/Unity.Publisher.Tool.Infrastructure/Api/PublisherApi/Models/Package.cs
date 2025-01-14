using System.Text.Json.Serialization;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

internal sealed class Package
{
    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("average_rating")]
    public required int Rating { get; init; }
}
