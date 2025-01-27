using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Unity.Publisher.Tool.Access;
using Unity.Publisher.Tool.Access.Options;
using AuthorizationOptions = Unity.Publisher.Tool.Access.Options.AuthorizationOptions;

namespace Unity.Publisher.Tool.Dependencies;

public static class AccessServiceInjection
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                GoogleAuthenticationOptions authOptions = configuration
                    .GetSection(GoogleAuthenticationOptions.JsonKey)
                    .Get<GoogleAuthenticationOptions>()!;

                options.Audience = authOptions.ClientId;
                options.Authority = authOptions.Issuer;

                TokenValidationParameters tokenValidation = new()
                {
                    RequireAudience = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

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
