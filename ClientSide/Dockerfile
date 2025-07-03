# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ClientSide/*.csproj ./ClientSide/
RUN dotnet restore ./ClientSide/ClientSide.csproj

COPY ClientSide/. ./ClientSide/
WORKDIR /src/ClientSide
RUN dotnet publish -c Release -o /app/publish

# run
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENV GRPC_SERVER=http://ordersservice:6006


ENTRYPOINT ["dotnet", "ClientSide.dll"]