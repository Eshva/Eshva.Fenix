# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY "Eshva.Fenix.BusinessUserBff.Service/Eshva.Fenix.BusinessUserBff.Service.csproj" .
RUN dotnet restore "Eshva.Fenix.BusinessUserBff.Service.csproj"
COPY "Eshva.Fenix.BusinessUserBff.Application/Eshva.Fenix.BusinessUserBff.Application.csproj" .
RUN dotnet restore "Eshva.Fenix.BusinessUserBff.Application.csproj"
COPY "Eshva.Fenix.BusinessUserBff.Models/Eshva.Fenix.BusinessUserBff.Models.csproj" .
RUN dotnet restore "Eshva.Fenix.BusinessUserBff.Models.csproj"
COPY . .
WORKDIR "/src/Eshva.Fenix.BusinessUserBff.Service"
RUN dotnet build "Eshva.Fenix.BusinessUserBff.Service.csproj" -c Release -o /app/build
RUN dotnet publish "Eshva.Fenix.BusinessUserBff.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Eshva.Fenix.BusinessUserBff.Service.dll"]
