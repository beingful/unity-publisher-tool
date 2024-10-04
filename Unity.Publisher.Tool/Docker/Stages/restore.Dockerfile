FROM mcr.microsoft.com/dotnet/sdk:8.0 as restore
WORKDIR /src
COPY ./*.sln .
COPY ./Unity.Publisher.Tool.Api/*.csproj ./Unity.Publisher.Tool.Api/
RUN dotnet restore