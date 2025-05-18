FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8070

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["YouTube.Mobile.Data/YouTube.Mobile.Data.csproj", "YouTube.Mobile.Data/"]
RUN dotnet restore "YouTube.Mobile.Data/YouTube.Mobile.Data.csproj"

COPY . .
WORKDIR "/src/YouTube.Mobile.Data"
RUN dotnet build "./YouTube.Mobile.Data.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YouTube.Mobile.Data.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YouTube.Mobile.Data.dll"]
