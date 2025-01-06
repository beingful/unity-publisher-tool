using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Infrastructure.Extensions;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

internal sealed class GetRevenueResponse : IConvertibleTo<Revenue>
{
    [JsonPropertyName("aaData")]
    public required string[][] MonthlyRevenue { get; init; }

    public Revenue Convert()
    {
        string[] currentRevenue = MonthlyRevenue.Last();

        return new Revenue(
            forPeriod: currentRevenue[2].ToPrice(),
            total: currentRevenue[4].ToPrice());
    }
}
