using Unity.Publisher.Tool.App.Models;

namespace Unity.Publisher.Tool.Endpoints.Responses;

public class PublisherActionResultResponse
{
    public required ActionResult[] Result { get; init; }
}
