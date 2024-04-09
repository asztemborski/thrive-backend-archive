FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Bootstrapper/Thrive.Bootstrapper/Thrive.Bootstrapper.csproj", "src/Bootstrapper/Thrive.Bootstrapper/"]
COPY ["src/Modules/Identity/Thrive.Modules.Identity.Api/Thrive.Modules.Identity.Api.csproj", "src/Modules/Identity/Thrive.Modules.Identity.Api/"]
COPY ["src/Modules/Identity/Thrive.Modules.Identity.Infrastructure/Thrive.Modules.Identity.Infrastructure.csproj", "src/Modules/Identity/Thrive.Modules.Identity.Infrastructure/"]
COPY ["src/Shared/Thrive.Shared.Infrastructure/Thrive.Shared.Infrastructure.csproj", "src/Shared/Thrive.Shared.Infrastructure/"]
COPY ["src/Shared/Thrive.Shared.Application/Thrive.Shared.Application.csproj", "src/Shared/Thrive.Shared.Application/"]
COPY ["src/Shared/Thrive.Shared.Abstractions/Thrive.Shared.Abstractions.csproj", "src/Shared/Thrive.Shared.Abstractions/"]
COPY ["src/Modules/Identity/Thrive.Modules.Identity.Application/Thrive.Modules.Identity.Application.csproj", "src/Modules/Identity/Thrive.Modules.Identity.Application/"]
COPY ["src/Modules/Identity/Thrive.Modules.Identity.Domain/Thrive.Modules.Identity.Domain.csproj", "src/Modules/Identity/Thrive.Modules.Identity.Domain/"]
COPY ["src/Modules/Identity/Thrive.Modules.Identity.Shared/Thrive.Modules.Identity.Shared.csproj", "src/Modules/Identity/Thrive.Modules.Identity.Shared/"]
COPY ["src/Modules/Notifications/Thrive.Modules.Notifications.Api/Thrive.Modules.Notifications.Api.csproj", "src/Modules/Notifications/Thrive.Modules.Notifications.Api/"]
COPY ["src/Modules/Notifications/Thrive.Modules.Notifications.Core/Thrive.Modules.Notifications.Core.csproj", "src/Modules/Notifications/Thrive.Modules.Notifications.Core/"]
RUN dotnet restore "src/Bootstrapper/Thrive.Bootstrapper/Thrive.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Thrive.Bootstrapper"
RUN dotnet build "Thrive.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Thrive.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thrive.Bootstrapper.dll"]
