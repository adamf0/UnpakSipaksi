#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN apt-get update && \
    apt-get install -y --no-install-recommends && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#mcr.microsoft.com/dotnet/sdk:8.0
FROM cgr.dev/chainguard/dotnet-sdk AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UnpakSipaksi/UnpakSipaksi.csproj", "UnpakSipaksi/"]
RUN dotnet restore "./UnpakSipaksi/UnpakSipaksi.csproj"
COPY . .
WORKDIR "/src/UnpakSipaksi"
RUN dotnet build "./UnpakSipaksi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UnpakSipaksi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnpakSipaksi.dll"]