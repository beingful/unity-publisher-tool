using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Infrastructure.Extensions;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

internal sealed class GetSalesResponse : IConvertibleTo<Sales>
{
    [JsonPropertyName("aaData")]
    public required string[][] Sales { get; init; }

    [JsonPropertyName("result")]
    public required NetIncome[] TotalNet { get; init; }

    public Sales Convert()
    {
        Sale[] sales = new Sale[Sales.Length];

        for (int i = 0; i < Sales.Length; i++)
        {
            string[] sale = Sales[i];

            sales[i] = Sale.Create(
                productTag: new ProductTag(
                    product: sale[0],
                    price: sale[1].ToPrice()),
                copiesHanded: int.Parse(sale[2]),
                revenue: TotalNet[i].Value.ToPrice(),
                loss: new Loss(
                    refund: int.Parse(sale[3]),
                    chargeback: int.Parse(sale[4])));
        }

        return new Sales(sales);
    }
}
