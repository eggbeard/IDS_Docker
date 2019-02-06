FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/TokenServer/TokenServer.csproj src/TokenServer/
RUN dotnet restore src/TokenServer/TokenServer.csproj
COPY . .
WORKDIR /src/src/TokenServer
RUN dotnet build TokenServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TokenServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TokenServer.dll"]
