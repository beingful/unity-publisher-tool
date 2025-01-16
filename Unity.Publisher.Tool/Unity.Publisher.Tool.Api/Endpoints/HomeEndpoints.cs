namespace Unity.Publisher.Tool.Endpoints;

public static class HomeEndpoints
{
    public static IEndpointRouteBuilder AddHomeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.GetHomePage();
    }

    private static IEndpointRouteBuilder GetHomePage(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");

            return Task.CompletedTask;
        })
        .ExcludeFromDescription();

        return endpoints;
    }
}
