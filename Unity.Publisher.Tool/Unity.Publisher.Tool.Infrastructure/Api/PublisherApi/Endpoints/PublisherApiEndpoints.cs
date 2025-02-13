namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal static class PublisherApiEndpoints
{
    private const string _logInPage = "https://id.unity.com/auth/genesis_login";
    private const string _salesPage = "https://publisher.assetstore.unity3d.com/sales.html";
    private const string _publisherProfile = "/user/overview.json";
    private const string _publisherInfo = "publisher/overview.json";
    private const string _packages = "management/packages.json";
    private const string _revenue = "publisher-info/revenue/{0}.json";
    private const string _sales = "publisher-info/sales/{0}/{1}.json";
    private const string _downloads = "publisher-info/downloads/{0}/{1}.json?package_filter=all";
    private const string _reviews = "publisher-info/reviews/{0}.json?page={1}&rows={2}&order_key=date&sort=desc";

    public static string LogInPage()
    {
        return _logInPage;
    }

    public static string SalesPage()
    {
        return _salesPage;
    }

    public static string PublisherProfile()
    {
        return _publisherProfile;
    }

    public static string PublisherInfo()
    {
        return _publisherInfo;
    }

    public static string Packages()
    {
        return _packages;
    }

    public static string Revenue(long publisherId)
    {
        return string.Format(_revenue, publisherId);
    }

    public static string Sales(long publisherId, DateTime date)
    {
        return string.Format(_sales, publisherId, new DateTimeUrlParameter(date));
    }

    public static string Downloads(long publisherId, DateTime date)
    {
        return string.Format(_downloads, publisherId, new DateTimeUrlParameter(date));
    }

    public static string Reviews(long publisherId, int page, int rows = 2)
    {
        return string.Format(_reviews, publisherId, page, rows);
    }
}
