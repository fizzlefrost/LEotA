#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LEotA.Ng/LEotA.csproj", "LEotA.Ng/"]
RUN dotnet restore "LEotA.Ng/LEotA.csproj"
COPY . .
WORKDIR "/src/LEotA.Ng"
RUN dotnet build "LEotA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LEotA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LEotA.dll"]