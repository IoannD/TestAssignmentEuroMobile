
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TestAssignmentEuroMobile.API/TestAssignmentEuroMobile.API.csproj", "TestAssignmentEuroMobile.API/"]
COPY ["TestAssignmentEuroMobile.Application/TestAssignmentEuroMobile.Application.csproj", "TestAssignmentEuroMobile.Application/"]
COPY ["TestAssignmentEuroMobile.Domain/TestAssignmentEuroMobile.Domain.csproj", "TestAssignmentEuroMobile.Domain/"]
RUN dotnet restore "./TestAssignmentEuroMobile.API/TestAssignmentEuroMobile.API.csproj"
COPY . .
WORKDIR "/src/TestAssignmentEuroMobile.API"
RUN dotnet build "./TestAssignmentEuroMobile.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TestAssignmentEuroMobile.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestAssignmentEuroMobile.API.dll"]