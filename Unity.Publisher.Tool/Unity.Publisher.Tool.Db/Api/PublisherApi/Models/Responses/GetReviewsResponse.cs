using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

internal sealed class GetReviewsResponse : IConvertibleTo<Reviews>
{
    [JsonPropertyName("reviews")]
    public required AssetReview[] Reviews { get; init; }

    public Reviews Convert()
    {
        Review[] reviews = new Review[Reviews.Length];

        for (int i = 0; i < reviews.Length; i++)
        {
            reviews[i] = new Review(
                Reviews[i].Id,
                Reviews[i].Asset,
                Reviews[i].Subject,
                Reviews[i].Body,
                Reviews[i].Rating,
                Reviews[i].Created);
        }

        return new Reviews(reviews);
    }
}
