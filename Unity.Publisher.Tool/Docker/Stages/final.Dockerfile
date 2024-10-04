ARG IMAGE
ARG BUILD_ID

FROM ${IMAGE}:publish-${BUILD_ID} AS publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER app
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Unity.Publisher.Tool.Api.dll"]