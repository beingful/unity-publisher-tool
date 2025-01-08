using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

internal sealed class GetPublisherOverviewResponse : IConvertibleTo<Rating>
{
    [JsonPropertyName("overview")]
    public required Overview Overview { get; init; }

    public Rating Convert()
    {
        return new Rating(
            Overview.PublisherRating.Count,
            Overview.PublisherRating.Average);
    }
}
