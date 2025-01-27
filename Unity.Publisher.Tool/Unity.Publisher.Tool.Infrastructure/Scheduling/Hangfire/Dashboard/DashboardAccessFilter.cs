using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Dashboard;

public class DashboardAccessFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        HttpContext httpContext = context.GetHttpContext();

        return httpContext.User.Identity?.IsAuthenticated == true;
    }
}
