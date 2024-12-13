using Unity.Publisher.Tool.Infrastructure.Http.Responses.Html.HtmlAgilityPack;

namespace Unity.Publisher.Tool.Infrastructure.Http.Responses.Html;

public sealed class HtmlContent : IHtmlContent
{
    private readonly IHtmlSearch _search;

    public HtmlContent(Stream htmlContent)
    {
        _search = new HtmlSearch(htmlContent);
    }

    public IHtmlSearch SearchDocument()
    {
        return _search;
    }
}
