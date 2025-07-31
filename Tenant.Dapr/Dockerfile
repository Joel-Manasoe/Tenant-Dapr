# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY Common/Common.csproj Common/
COPY Tenant.Dapr/Tenant.Dapr.csproj Tenant.Dapr/

# Restore dependencies
RUN dotnet restore Tenant.Dapr/Tenant.Dapr.csproj

# Copy full source
COPY Common/ Common/
COPY Tenant.Dapr/ Tenant.Dapr/

# Build the project
WORKDIR /src/Tenant.Dapr
RUN dotnet build Tenant.Dapr.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Tenant.Dapr.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tenant.Dapr.dll"]
