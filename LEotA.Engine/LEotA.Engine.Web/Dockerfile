#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM bfren/aspnet:net5.0.17 AS base
WORKDIR /app
EXPOSE 10001
ENV ASPNETCORE_URLS=http://+:10001

FROM bitnami/dotnet-sdk:5.0.408 AS build
WORKDIR /src
COPY ["LEotA.Engine/LEotA.Engine.Web/LEotA.Engine.Web.csproj", "LEotA.Engine/LEotA.Engine.Web/"]
COPY ["LEotA.Engine/LEotA.Engine.Data/LEotA.Engine.Data.csproj", "LEotA.Engine/LEotA.Engine.Data/"]
COPY ["LEotA.Engine/LEotA.Engine.Entities/LEotA.Engine.Entities.csproj", "LEotA.Engine/LEotA.Engine.Entities/"]
RUN dotnet restore "LEotA.Engine/LEotA.Engine.Web/LEotA.Engine.Web.csproj"
COPY . .
WORKDIR "/src/LEotA.Engine/LEotA.Engine.Web"
RUN dotnet build "LEotA.Engine.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LEotA.Engine.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LEotA.Engine.Web.dll"]