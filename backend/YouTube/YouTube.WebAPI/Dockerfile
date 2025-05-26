FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["YouTube.WebAPI/YouTube.WebAPI.csproj", "YouTube.WebAPI/"]
COPY ["YouTube.Application/YouTube.Application.csproj", "YouTube.Application/"]
COPY ["YouTube.Infrastructure/YouTube.Infrastructure.csproj", "YouTube.Infrastructure/"]
COPY ["YouTube.Data.S3/YouTube.Data.S3.csproj", "YouTube.Data.S3/"]
COPY ["YouTube.Domain/YouTube.Domain.csproj", "YouTube.Domain/"]
COPY ["YouTube.Persistence/YouTube.Persistence.csproj", "YouTube.Persistence/"]
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
