using System.Text.Json.Serialization;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

public class PublisherRating
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("average")]
    public double Average { get; set; }
}
