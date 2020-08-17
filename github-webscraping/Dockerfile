
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["github-webscraping.csproj", "github-webscraping/"]
RUN dotnet restore "github-webscraping/github-webscraping.csproj"
WORKDIR "/src/github-webscraping"
COPY . .
RUN dotnet build "github-webscraping.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "github-webscraping.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet github-webscraping.dll