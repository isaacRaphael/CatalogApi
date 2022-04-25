FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS server
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["CatalogApi.csproj", "."]
RUN dotnet restore "CatalogApi.csproj"
COPY . .
RUN dotnet build "CatalogApi.csproj" -c Release -o /app/build
FROM build AS pulbish
RUN dotnet publish "CatalogApi.csproj" -c Release -o /app/publish
FROM server AS final
WORKDIR /app
COPY --from=pulbish /app/publish .
CMD ["dotnet", "CatalogApi.dll"]