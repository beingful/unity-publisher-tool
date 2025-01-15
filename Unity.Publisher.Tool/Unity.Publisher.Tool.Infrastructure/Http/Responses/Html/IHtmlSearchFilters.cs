namespace Unity.Publisher.Tool.Infrastructure.Http.Responses.Html;

public interface IHtmlSearchFilters
{
    IHtmlSearchFilters Tag(string tag);

    IHtmlSearchFilters Attribute(string name, string value);

    IHtmlSearch BuildFilter();
}
