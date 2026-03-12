FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["SmartEvent.Backend.sln", "./"]
COPY ["SmartEvent.Backend.Api/SmartEvent.Backend.Api.csproj", "SmartEvent.Backend.Api/"]
COPY ["SmartEvent.Backend.Application/SmartEvent.Backend.Application.csproj", "SmartEvent.Backend.Application/"]
COPY ["SmartEvent.Backend.Core/SmartEvent.Backend.Core.csproj", "SmartEvent.Backend.Core/"]
COPY ["SmartEvent.Backend.Persistence/SmartEvent.Backend.Persistence.csproj", "SmartEvent.Backend.Persistence/"]

RUN dotnet restore "SmartEvent.Backend.Api/SmartEvent.Backend.Api.csproj"

COPY . .

WORKDIR "/src/SmartEvent.Backend.Api"
RUN dotnet publish "SmartEvent.Backend.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SmartEvent.Backend.Api.dll"]