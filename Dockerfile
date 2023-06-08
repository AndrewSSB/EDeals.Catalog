#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/EDeals.Catalog.API/EDeals.Catalog.API.csproj", "src/EDeals.Catalog.API/"]
COPY ["src/EDeals.Catalog.Infrastructure/EDeals.Catalog.Infrastructure.csproj", "src/EDeals.Catalog.Infrastructure/"]
COPY ["src/EDeals.Catalog.Application/EDeals.Catalog.Application.csproj", "src/EDeals.Catalog.Application/"]
COPY ["src/EDeals.Catalog.Domain/EDeals.Catalog.Domain.csproj", "src/EDeals.Catalog.Domain/"]
RUN dotnet restore "src/EDeals.Catalog.API/EDeals.Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/EDeals.Catalog.API"
RUN dotnet build "EDeals.Catalog.API.csproj" -c Release --no-restore

RUN dotnet publish "EDeals.Catalog.API.csproj" -c Release -o --no-build --output /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EDeals.Catalog.API.dll"]