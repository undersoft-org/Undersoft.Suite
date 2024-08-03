#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 15015
EXPOSE 16016

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.Server/Undersoft.GDC.Service.Application.Server.csproj", "src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.Server/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Application.Server/Undersoft.SDK.Service.Application.Server.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Application.Server/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Application/Undersoft.SDK.Service.Application.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Application/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service/Undersoft.SDK.Service.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK/Undersoft.SDK.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Infrastructure/Undersoft.SDK.Service.Infrastructure.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Infrastructure/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Server/Undersoft.SDK.Service.Server.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Server/"]
COPY ["src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.Client/Undersoft.GDC.Service.Application.Client.csproj", "src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.Client/"]
COPY ["src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.GUI/Undersoft.GDC.Service.Application.GUI.csproj", "src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.GUI/"]
COPY ["src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Application.GUI/Undersoft.SDK.Service.Application.GUI.csproj", "src/SoftwareDevelopmentKit/src/Undersoft.SDK.Service.Application.GUI/"]
COPY ["src/SharedServiceCenter/src/Undersoft.GDC.Service.Application/Undersoft.GDC.Service.Application.csproj", "src/SharedServiceCenter/src/Undersoft.GDC.Service.Application/"]
COPY ["src/SharedServiceCenter/src/Undersoft.GDC.Service/Undersoft.GDC.Service.csproj", "src/SharedServiceCenter/src/Undersoft.GDC.Service/"]
COPY ["src/SharedServiceCenter/src/Undersoft.GDC/Undersoft.GDC.csproj", "src/SharedServiceCenter/src/Undersoft.GDC/"]
COPY ["src/SharedServiceCenter/src/Undersoft.GDC.Service.Infrastructure/Undersoft.GDC.Service.Infrastructure.csproj", "src/SharedServiceCenter/src/Undersoft.GDC.Service.Infrastructure/"]
RUN dotnet restore ./src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.Server/Undersoft.GDC.Service.Application.Server.csproj
COPY . .
WORKDIR /src/src/SharedServiceCenter/src/Undersoft.GDC.Service.Application.Server
RUN dotnet build ./Undersoft.GDC.Service.Application.Server.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./Undersoft.GDC.Service.Application.Server.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Undersoft.GDC.Service.Application.Server.dll"]