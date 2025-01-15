ARG IMAGE
ARG BUILD_ID

FROM ${IMAGE}:build-${BUILD_ID}

RUN dotnet test ./Tests/**/*.csproj