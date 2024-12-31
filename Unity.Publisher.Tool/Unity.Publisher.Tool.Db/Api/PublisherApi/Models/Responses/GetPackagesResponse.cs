using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

internal sealed class GetPackagesResponse : IConvertibleTo<Assets>
{
    [JsonPropertyName("packages")]
    public required Package[] Packages { get; init; }

    public Assets Convert()
    {
        Asset[] assets = new Asset[Packages.Length];

        for (int i = 0; i < assets.Length; i++)
        {
            assets[i] = new Asset(Packages[i].Id, Packages[i].Name, Packages[i].Rating);
        }

        return new Assets(assets);
    }
}
