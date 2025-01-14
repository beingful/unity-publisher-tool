using HtmlAgilityPack;

namespace Unity.Publisher.Tool.Infrastructure.Http.Responses.Html.HtmlAgilityPack;

public delegate IEnumerable<HtmlNode> HtmlElementSearchFilter(IEnumerable<HtmlNode> nodes);

public sealed class HtmlSearch : IHtmlSearch, IHtmlSearchFilters
{
    private readonly IEnumerable<HtmlNode> _htmlNodes;
    private List<HtmlElementSearchFilter> _filters = [];

    public HtmlSearch(Stream htmlContent)
    {
        HtmlDocument htmlDocument = new();

        htmlDocument.Load(htmlContent);

        _htmlNodes = htmlDocument.DocumentNode.Descendants();
    }

    IHtmlSearchFilters IHtmlSearch.FilterBy() => this;

    IHtmlSearchFilters IHtmlSearchFilters.Attribute(string name, string value)
    {
        return AddFilter(elementMatchCondition: htmlElement =>
        {
            return htmlElement.GetAttributeValue(name, null) == value;
        });
    }

    IHtmlSearchFilters IHtmlSearchFilters.Tag(string tag)
    {
        return AddFilter(elementMatchCondition: htmlElement =>
        {
            return htmlElement.Name == tag;
        });
    }

    IHtmlSearch IHtmlSearchFilters.BuildFilter() => this;

    string? IHtmlSearch.FetchAttributeValue(string targetAttribute)
    {
        HtmlNode? htmlNode = FindFirstOrDefault();

        ClearFilters();

        return htmlNode?.GetAttributeValue(targetAttribute, null);
    }

    private IHtmlSearchFilters AddFilter(Func<HtmlNode, bool> elementMatchCondition)
    {
        _filters.Add(htmlElements =>
        {
            return htmlElements.Where(node => elementMatchCondition.Invoke(node));
        });

        return this;
    }

    private HtmlNode? FindFirstOrDefault() => Find().FirstOrDefault();

    private IEnumerable<HtmlNode> Find()
    {
        IEnumerable<HtmlNode> matchingNodes = _htmlNodes;

        foreach (HtmlElementSearchFilter filter in _filters)
        {
            matchingNodes = filter(matchingNodes);
        }

        return matchingNodes;
    }

    private void ClearFilters() => _filters.Clear();
}
