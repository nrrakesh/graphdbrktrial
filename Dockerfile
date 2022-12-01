#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV TZ="Asia/Calcutta"
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GraphDBIntegration.csproj", "."]
RUN dotnet restore "./GraphDBIntegration.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GraphDBIntegration.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "GraphDBIntegration.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphDBIntegration.dll"]