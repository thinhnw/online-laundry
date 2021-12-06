#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Devjobs.csproj", "Devjobs/"]

RUN dotnet restore "Devjobs/Devjobs.csproj"
COPY . T2008M_NetCoreApi
WORKDIR "/src/Devjobs"
RUN dotnet build "Devjobs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devjobs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "Devjobs.dll"]