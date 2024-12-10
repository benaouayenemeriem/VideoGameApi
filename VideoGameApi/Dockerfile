# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["VideoGameApi/VideoGameApi.csproj", "VideoGameApi/"]
RUN dotnet restore "./VideoGameApi/VideoGameApi.csproj"
COPY . .
WORKDIR "/src/VideoGameApi"
RUN dotnet build "./VideoGameApi.csproj" -c Release -o /app/build

# Install EF tools in the build container
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Publish stage
FROM build AS publish
RUN dotnet publish "./VideoGameApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoGameApi.dll"]
