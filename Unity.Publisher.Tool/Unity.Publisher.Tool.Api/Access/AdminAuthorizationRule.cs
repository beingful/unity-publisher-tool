using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Unity.Publisher.Tool.Access;

public class AdminAuthorizationRule : IAuthorizationRequirement
{
    private readonly Admin _admin;

    public AdminAuthorizationRule(Admin admin)
    {
        _admin = admin;
    }

    public bool Authorize(ClaimsPrincipal user)
    {
        return IsAuthenticated(user) && IsAdmin(user);
    }

    private bool IsAuthenticated(ClaimsPrincipal user)
    {
        return user.Identity?.IsAuthenticated == true;
    }

    private bool IsAdmin(ClaimsPrincipal user)
    {
        string usersEmail = user.FindFirstValue(ClaimTypes.Email)!;

        return _admin.Emails.Contains(usersEmail);
    }
}
