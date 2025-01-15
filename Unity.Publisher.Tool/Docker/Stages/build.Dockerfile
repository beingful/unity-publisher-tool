ARG IMAGE
ARG BUILD_ID

FROM ${IMAGE}:restore-${BUILD_ID}

ARG BUILD_CONFIGURATION

COPY *.sln .
COPY ./Unity.Publisher.Tool.Api/ ./Unity.Publisher.Tool.Api/
COPY ./Unity.Publisher.Tool.App/ ./Unity.Publisher.Tool.App/
COPY ./Unity.Publisher.Tool.Domain/ ./Unity.Publisher.Tool.Domain/
COPY ./Unity.Publisher.Tool.Infrastructure/ ./Unity.Publisher.Tool.Infrastructure/

RUN dotnet build --no-restore ./Unity.Publisher.Tool.Api/*.csproj -c ${BUILD_CONFIGURATION}