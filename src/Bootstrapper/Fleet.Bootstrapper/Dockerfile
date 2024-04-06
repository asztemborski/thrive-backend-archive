FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Bootstrapper/Fleet.Bootstrapper/Fleet.Bootstrapper.csproj", "src/Bootstrapper/Fleet.Bootstrapper/"]
COPY ["src/Shared/Fleet.Shared.Abstractions/Fleet.Shared.Abstractions.csproj", "src/Shared/Fleet.Shared.Abstractions/"]
COPY ["src/Shared/Fleet.Shared.Infrastructure/Fleet.Shared.Infrastructure.csproj", "src/Shared/Fleet.Shared.Infrastructure/"]
RUN dotnet restore "src/Bootstrapper/Fleet.Bootstrapper/Fleet.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Fleet.Bootstrapper"
RUN dotnet build "Fleet.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fleet.Bootstrapper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fleet.Bootstrapper.dll"]
