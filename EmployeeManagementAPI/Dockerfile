FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeManagementAPI/EmployeeManagement.Api.csproj", "EmployeeManagementAPI/"]
COPY ["EmployeeManagement.Domain/EmployeeManagement.Domain.csproj", "EmployeeManagement.Domain/"]
COPY ["EmployeeManagement.Shared/EmployeeManagement.Shared.csproj", "EmployeeManagement.Shared/"]
COPY ["EmployeeManagement/EmployeeManagement.Repository.csproj", "EmployeeManagement/"]
RUN dotnet restore "./EmployeeManagementAPI/EmployeeManagement.Api.csproj"
COPY . .
WORKDIR "/src/EmployeeManagementAPI"
RUN dotnet build "./EmployeeManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployeeManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final (
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EmployeeManagement.Api.dll"]
