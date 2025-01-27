using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Unity.Publisher.Tool.Access;
using AuthorizationOptions = Unity.Publisher.Tool.Access.Options.AuthorizationOptions;

namespace Unity.Publisher.Tool.Dependencies;

public static class AccessServiceInjection
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMicrosoftIdentityWebApiAuthentication(
                configuration: configuration.GetSection("Authentication"),
                configSectionName: "Google",
                jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme);

        return services;
    }

    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuthorization(options =>
            {
                AuthorizationOptions authorizationOptions = configuration
                        .GetSection(AuthorizationOptions.JsonKey)
                        .Get<AuthorizationOptions>()!;

                AuthorizationPolicy policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAssertion(context =>
                    {
                        AdminAuthorizationRule adminRule = new(authorizationOptions.Admin);

                        return adminRule.Authorize(context.User);
                    })
                    .Build();

                options.DefaultPolicy = policy;

                options.AddPolicy(AuthorizationPolicies.Admin, policy);
            });
    }
}
