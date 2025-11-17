# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier le fichier csproj et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier tout le projet et publier
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copier les fichiers publiés
COPY --from=build /app/publish .

# Exposer le port attendu par Render
EXPOSE 10000

# Démarrer l'application
ENTRYPOINT ["dotnet", "AttendanceBackend.dll"]
