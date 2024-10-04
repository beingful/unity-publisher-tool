FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src
COPY ./*.sln .
COPY ./Unity.Publisher.Tool.Api/*.csproj ./Unity.Publisher.Tool.Api/
RUN dotnet restore

FROM restore AS build
ARG BUILD_CONFIGURATION=Release
COPY . .
RUN dotnet build --no-restore ./Unity.Publisher.Tool.Api/*.csproj -c $BUILD_CONFIGURATION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-build ./Unity.Publisher.Tool.Api/*.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unity.Publisher.Tool.Api.dll"]