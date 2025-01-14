using System.Text.Json.Serialization;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

internal sealed class AssetReview
{
    [JsonPropertyName("review_id")]
    public required long Id { get; init; }

    [JsonPropertyName("name")]
    public required string Asset { get; init; }

    [JsonPropertyName("package_id")]
    public required int AssetId { get; init; }

    [JsonPropertyName("body")]
    public required string Body { get; init; }

    [JsonPropertyName("subject")]
    public required string Subject { get; init; }

    [JsonPropertyName("rating")]
    public required byte Rating { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime Created { get; init; }
}
