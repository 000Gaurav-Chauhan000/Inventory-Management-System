FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PurchaseOrderService/PurchaseOrderService.csproj", "PurchaseOrderService/"]
RUN dotnet restore "./PurchaseOrderService/PurchaseOrderService.csproj"
COPY . .
WORKDIR "/src/PurchaseOrderService"
RUN dotnet build "./PurchaseOrderService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PurchaseOrderService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurchaseOrderService.dll"]
