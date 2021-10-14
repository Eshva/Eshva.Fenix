# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["Eshva.OpenApiAndMongoDb.Bff.Service/Eshva.OpenApiAndMongoDb.Bff.Service.csproj", "."]
RUN dotnet restore "Eshva.OpenApiAndMongoDb.Bff.Service.csproj"
COPY . .
WORKDIR "/src/Eshva.OpenApiAndMongoDb.Bff.Service"
RUN dotnet build "Eshva.OpenApiAndMongoDb.Bff.Service.csproj" -c Release -o /app/build
RUN dotnet publish "Eshva.OpenApiAndMongoDb.Bff.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Eshva.OpenApiAndMongoDb.Bff.Service.dll"]
