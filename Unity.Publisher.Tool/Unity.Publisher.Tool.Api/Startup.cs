using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using System.Text.Json.Serialization;
using Unity.Publisher.Tool.Dependencies;
using Unity.Publisher.Tool.Endpoints;

namespace Unity.Publisher.Tool;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddAuthentication(Configuration)
            .AddAuthorization(Configuration)
            .ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddScheduler(Configuration)
            .AddConfigurationOptions(Configuration);
    }

    public void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder
            .AddApplicationSrevices()
            .AddInfrastructureServices()
            .AddDomainServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .AddHomeEndpoints()
                .AddNotificationEndpoints();
        });

        app.UseDashboard(Configuration);

        GlobalConfiguration.Configuration.UseAutofacActivator(app.ApplicationServices.GetAutofacRoot());
    }
}
