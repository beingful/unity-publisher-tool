FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine

WORKDIR /src

COPY ./Unity.Publisher.Tool.Api/*.csproj ./Unity.Publisher.Tool.Api/
COPY ./Unity.Publisher.Tool.App/*.csproj ./Unity.Publisher.Tool.App/
COPY ./Unity.Publisher.Tool.Domain/*.csproj ./Unity.Publisher.Tool.Domain/
COPY ./Unity.Publisher.Tool.Infrastructure/*.csproj ./Unity.Publisher.Tool.Infrastructure/

RUN dotnet restore ./Unity.Publisher.Tool.Api/*.csproj