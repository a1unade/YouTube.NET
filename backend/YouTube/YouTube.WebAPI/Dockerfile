FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["YouTube.WebAPI/YouTube.WebAPI.csproj", "YouTube.WebAPI/"]
COPY ["YouTube.Persistence/YouTube.Persistence.csproj", "YouTube.Persistence/"]
COPY ["YouTube.Infrastructure/YouTube.Infrastructure.csproj", "YouTube.Infrastructure/"]
COPY ["YouTube.Domain/YouTube.Domain.csproj", "YouTube.Domain/"]
COPY ["YouTube.Application/YouTube.Application.csproj", "YouTube.Application/"]
RUN dotnet restore "YouTube.WebAPI/YouTube.WebAPI.csproj"

COPY . .
WORKDIR "/src/YouTube.WebAPI"
RUN dotnet build "YouTube.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YouTube.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YouTube.WebAPI.dll"]
