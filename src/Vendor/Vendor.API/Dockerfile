FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 8001

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/Vendor/Vendor.API/Vendor.API.csproj src/Vendor/Vendor.API/
RUN dotnet restore src/Vendor/Vendor.API/Vendor.API.csproj
COPY . .
WORKDIR /src/src/Vendor/Vendor.API
RUN dotnet build Vendor.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Vendor.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Vendor.API.dll"]
