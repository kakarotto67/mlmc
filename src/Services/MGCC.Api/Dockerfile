FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src

# Copy csproj into image
COPY ["src/Services/MGCC.Api/MGCC.Api.csproj", "/src/Services/MGCC.Api/"]
COPY ["src/Services/EnterpriseServiceBus/EnterpriseServiceBus.csproj", "/src/Services/EnterpriseServiceBus/"]
COPY ["src/Services/Shared/Shared.csproj", "/src/Services/Shared/"]

# Restore csproj in the image
RUN dotnet restore "/src/Services/MGCC.Api/MGCC.Api.csproj"

WORKDIR "/src/Services/MGCC.Api/"

# Copy restored stuff into image
COPY ["src/Services/MGCC.Api", "/src/Services/MGCC.Api/"]
COPY ["src/Services/EnterpriseServiceBus", "/src/Services/MGCC.Api/"]
COPY ["src/Services/Shared", "/src/Services/MGCC.Api/"]

# Additionally, copy ESB settings into image so related service will be able to find it
COPY ["src/Services/EnterpriseServiceBus/esb-settings.json", "/src/Services/EnterpriseServiceBus/"]

WORKDIR "/src/Services/MGCC.Api"
RUN dotnet build "MGCC.Api.csproj" -c Release -o /app/build

FROM build AS publish

COPY ["src/Services/EnterpriseServiceBus/esb-settings.json", "/EnterpriseServiceBus/"]

RUN dotnet publish "MGCC.Api.csproj" -c Release -o /app/publish

FROM base AS final

COPY ["src/Services/EnterpriseServiceBus/esb-settings.json", "/EnterpriseServiceBus/"]

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mlmc.MGCC.Api.dll"]