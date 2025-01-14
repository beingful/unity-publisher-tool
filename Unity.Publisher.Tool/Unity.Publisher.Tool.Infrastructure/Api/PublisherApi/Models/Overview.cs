using System.Text.Json.Serialization;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

internal sealed class Overview
{
    [JsonPropertyName("rating")]
    public required PublisherRating PublisherRating { get; init; }
}
