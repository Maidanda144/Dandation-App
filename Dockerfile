# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copier les fichiers du projet
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exposer le port utilisé par Render
EXPOSE 10000

# Lancer l'application
ENTRYPOINT ["dotnet", "AttendanceBackend.dll"]
