FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fiap.Web.Students/Fiap.Web.Students.csproj", "Fiap.Web.Students/"]
RUN dotnet restore "Fiap.Web.Students/Fiap.Web.Students.csproj"
COPY . .
WORKDIR "/src/Fiap.Web.Students"
RUN dotnet build "./Fiap.Web.Students.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fiap.Web.Students.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Web.Students.dll"]
