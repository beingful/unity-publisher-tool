ARG IMAGE
ARG BUILD_ID

FROM ${IMAGE}:restore-${BUILD_ID}

ARG BUILD_CONFIGURATION

COPY *.sln .
COPY Unity.Publisher.Tool.*/ ./

RUN dotnet build --no-restore ./Unity.Publisher.Tool.Api/*.csproj -c ${BUILD_CONFIGURATION}