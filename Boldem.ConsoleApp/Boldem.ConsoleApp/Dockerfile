FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Boldem.ConsoleApp/Boldem.ConsoleApp.csproj", "Boldem.ConsoleApp/"]
RUN dotnet restore "Boldem.ConsoleApp/Boldem.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/Boldem.ConsoleApp"
RUN dotnet build "Boldem.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Boldem.ConsoleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boldem.ConsoleApp.dll"]
