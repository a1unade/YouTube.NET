FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8085

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["YouTube.Payment/YouTube.Payment.csproj", "YouTube.Payment/"]
COPY ["YouTube.Payment.Data/YouTube.Payment.Data.csproj", "YouTube.Payment.Data/"]
RUN dotnet restore "YouTube.Payment/YouTube.Payment.csproj"

COPY . .
WORKDIR "/src/YouTube.Payment"
RUN dotnet build "YouTube.Payment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YouTube.Payment.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YouTube.Payment.dll"]
