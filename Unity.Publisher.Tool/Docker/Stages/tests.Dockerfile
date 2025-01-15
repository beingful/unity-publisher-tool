ARG IMAGE
ARG BUILD_ID

FROM ${IMAGE}:build-${BUILD_ID}

COPY ./Tests ./Tests

RUN dotnet test ./Tests/**/*.csproj