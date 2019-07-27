FROM 076880417388.dkr.ecr.cn-northwest-1.amazonaws.com.cn/images:dotnet-core-sdk-2.2-alpine AS build
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


FROM 076880417388.dkr.ecr.cn-northwest-1.amazonaws.com.cn/images:dotnet-core-aspnet-2.2-alpine AS runtime
WORKDIR /app
COPY --from=build /app/src/order-service/out ./
ENTRYPOINT ["dotnet", "order-service.dll"]
EXPOSE 5000