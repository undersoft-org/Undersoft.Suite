#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 25025
EXPOSE 26026

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SharedServiceCenter/src/Undersoft.SSC.Service.Server/Undersoft.SSC.Service.Server.csproj", "src/SharedServiceCenter/src/Undersoft.SSC.Service.Server/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Server/Undersoft.SDK.Service.Server.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Server/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Infrastructure/Undersoft.SDK.Service.Infrastructure.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Infrastructure/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service/Undersoft.SDK.Service.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK/Undersoft.SDK.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK/"]
COPY ["src/SharedServiceCenter/src/Undersoft.SSC.Service.Infrastructure/Undersoft.SSC.Service.Infrastructure.csproj", "src/SharedServiceCenter/src/Undersoft.SSC.Service.Infrastructure/"]
COPY ["src/SharedServiceCenter/src/Undersoft.SSC.Service/Undersoft.SSC.Service.csproj", "src/SharedServiceCenter/src/Undersoft.SSC.Service/"]
COPY ["src/SharedServiceCenter/src/Undersoft.SSC/Undersoft.SSC.csproj", "src/SharedServiceCenter/src/Undersoft.SSC/"]
RUN dotnet restore ./src/SharedServiceCenter/src/Undersoft.SSC.Service.Server/Undersoft.SSC.Service.Server.csproj
COPY . .
WORKDIR /src/src/SharedServiceCenter/src/Undersoft.SSC.Service.Server
RUN dotnet build ./Undersoft.SSC.Service.Server.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./Undersoft.SSC.Service.Server.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Undersoft.SSC.Service.Server.dll"]