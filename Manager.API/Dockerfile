FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Manager/Manager.API/Manager.API.csproj", "Manager/Manager.API/"]
RUN dotnet restore "Manager.API/Manager.API.csproj"
COPY . .
WORKDIR "/src/Manager.API"
RUN dotnet build "Manager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Manager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Manager.API.dll"]
