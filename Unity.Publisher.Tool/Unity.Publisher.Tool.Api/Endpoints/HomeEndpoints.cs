using Unity.Publisher.Tool.Endpoints.Responses;

namespace Unity.Publisher.Tool.Endpoints;

public static class HomeEndpoints
{
    public static IEndpointRouteBuilder AddHomeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .GetHomePage()
            .GetClaims();
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

    private static IEndpointRouteBuilder GetClaims(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/claims", (HttpContext context) =>
        {
            string headers = "All headers:";

            context.Request.Headers.ToList().ForEach(header => headers += $@"
            ({header.Key}, {header.Value})
            ");

            string claims = "All claims:";

            context.User.Claims.ToList().ForEach(claim => claims += $@"
            ({claim.Type}, {claim.Value})
            ");

            return Results.Ok(new { Value = headers + '\n' + claims });
        });

        return endpoints;
    }
}
