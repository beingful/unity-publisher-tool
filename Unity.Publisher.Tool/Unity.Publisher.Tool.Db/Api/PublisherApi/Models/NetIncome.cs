using System.Text.Json.Serialization;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

public sealed class NetIncome
{
    [JsonPropertyName("net")]
    public required string Value { get; init; }
}
