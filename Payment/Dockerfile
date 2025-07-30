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
COPY Payment/Payment.csproj Payment/

# Restore dependencies
RUN dotnet restore Payment/Payment.csproj

# Copy everything else
COPY Common/ Common/
COPY Payment/ Payment/

# Build the project
WORKDIR /src/Payment
RUN dotnet build Payment.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Payment.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.dll"]
