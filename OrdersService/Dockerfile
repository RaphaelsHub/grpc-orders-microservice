# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY OrdersService/*.csproj ./OrdersService/
RUN dotnet restore ./OrdersService/OrdersService.csproj

COPY OrdersService/. ./OrdersService/
WORKDIR /src/OrdersService
RUN dotnet publish -c Release -o /app/publish


# run
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# gRPC требует HTTP/2
EXPOSE 6006

ENV ASPNETCORE_URLS=http://+:6006
ENTRYPOINT ["dotnet", "OrdersService.dll"]