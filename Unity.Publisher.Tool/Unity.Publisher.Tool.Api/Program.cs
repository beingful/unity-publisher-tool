using Autofac;
using Autofac.Extensions.DependencyInjection;
using Unity.Publisher.Tool;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

Startup startup = new(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        startup.ConfigureContainer(containerBuilder);
    });

WebApplication webApp = builder.Build();

startup.Configure(webApp, webApp.Environment);

webApp.Run();
