# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files only
COPY Common/Common.csproj Common/
COPY PaymentReceived/PaymentReceived.csproj PaymentReceived/

# Restore dependencies
RUN dotnet restore PaymentReceived/PaymentReceived.csproj

# Copy full source for only necessary projects
COPY Common/ Common/
COPY PaymentReceived/ PaymentReceived/

# Build the project
WORKDIR /src/PaymentReceived
RUN dotnet build PaymentReceived.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish PaymentReceived.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentReceived.dll"]
