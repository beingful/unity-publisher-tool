using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

internal sealed class GetDownloadsResponse : IConvertible<Downloads>
{
    [JsonPropertyName("aaData")]
    public required string[][] AssetDownloads { get; init; }

    public Downloads Convert()
    {
        Download[] downloads = new Download[AssetDownloads.Length];

        for (int i = 0; i < downloads.Length; i++)
        {
            string[] downloadsDetails = AssetDownloads[i];

            downloads[i] = new Download(
                product: downloadsDetails[0],
                downloads: int.Parse(downloadsDetails[1]),
                downloaders: int.Parse(downloadsDetails[2]));
        }

        return new Downloads(downloads);
    }
}
