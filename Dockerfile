FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/order-service/*.csproj ./src/order-service/
COPY test/order-service-test/*.csproj ./test/order-service-test/
RUN dotnet restore

# copy everything else and build app
COPY src/order-service/. ./src/order-service/
COPY test/order-service-test/. ./test/order-service-test/
WORKDIR /app/src/order-service
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/src/order-service/out ./
ENTRYPOINT ["dotnet", "order-service.dll"]
EXPOSE 5000