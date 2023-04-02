# Documentation: https://hub.docker.com/_/microsoft-dotnet-sdk/
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ordermanager-dotnet.csproj", "./"]
RUN dotnet restore "ordermanager-dotnet.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ordermanager-dotnet.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "ordermanager-dotnet.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ordermanager-dotnet.dll"]
