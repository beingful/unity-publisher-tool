using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.Redis.StackExchange;
using StackExchange.Redis;
using Unity.Publisher.Tool.Dependencies;
using Unity.Publisher.Tool.Endpoints;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = "Production"
});

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddHangfire((sp, configuration) =>
    {
        configuration.UseRedisStorage(sp.GetRequiredService<ConnectionMultiplexer>());
    })
    .AddHangfireServer()
    .AddConfigurationOptions(builder);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {
        container
            .AddApplicationSrevices()
            .AddInfrastructureServices()
            .AddDomainServices();
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.AddNotificationEndpoints();

app.Run();
