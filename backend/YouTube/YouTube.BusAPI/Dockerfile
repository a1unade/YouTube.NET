FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["YouTube.BusAPI/YouTube.BusAPI.csproj", "YouTube.BusAPI/"]
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
