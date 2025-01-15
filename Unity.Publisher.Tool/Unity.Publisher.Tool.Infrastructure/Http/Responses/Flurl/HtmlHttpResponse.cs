using Flurl.Http;
using HtmlAgilityPack;
using Unity.Publisher.Tool.Infrastructure.Http.Responses.Html;

namespace Unity.Publisher.Tool.Infrastructure.Http.Responses.Flurl;

public sealed class HtmlHttpResponse : HttpResponse, IHtmlHttpResponse
{
    private readonly HtmlContent _content;

    public HtmlHttpResponse(IFlurlResponse response) : base(response)
    {
        HtmlDocument htmlDocument = new();

        _content = new HtmlContent(response.ResponseMessage.Content.ReadAsStream());
    }

    public IHtmlContent Content => _content;
}
