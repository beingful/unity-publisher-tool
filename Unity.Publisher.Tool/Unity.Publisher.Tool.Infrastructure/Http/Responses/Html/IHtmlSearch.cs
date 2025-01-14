namespace Unity.Publisher.Tool.Infrastructure.Http.Responses.Html;

public interface IHtmlSearch
{
    IHtmlSearchFilters FilterBy();

    string? FetchAttributeValue(string targetAttribute);
}
