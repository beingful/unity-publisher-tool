using Unity.Publisher.Tool.Dependencies;
using Unity.Publisher.Tool.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddNotificationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddNotificationEndpoints();

app.Run();
