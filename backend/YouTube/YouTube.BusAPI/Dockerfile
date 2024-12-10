FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8090

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["YouTube.BusAPI/YouTube.BusAPI.csproj", "YouTube.BusAPI/"]
COPY ["YouTube.Application/YouTube.Application.csproj", "YouTube.Application/"]
COPY ["YouTube.Infrastructure/YouTube.Infrastructure.csproj", "YouTube.Infrastructure/"]
RUN dotnet restore "YouTube.BusAPI/YouTube.BusAPI.csproj"

COPY . .
WORKDIR "/src/YouTube.BusAPI"
RUN dotnet build "YouTube.BusAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YouTube.BusAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YouTube.BusAPI.dll"]
