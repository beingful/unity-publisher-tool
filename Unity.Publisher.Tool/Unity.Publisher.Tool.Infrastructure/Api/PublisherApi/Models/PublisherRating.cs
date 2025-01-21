using System.Text.Json.Serialization;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

public class PublisherRating
{
    [JsonPropertyName("average")]
    public double Average { get; set; }
}
