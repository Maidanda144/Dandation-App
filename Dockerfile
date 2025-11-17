# ------------------------------
# Étape 1 : Build
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier les fichiers du projet et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier tout le projet
COPY . ./

# Publier l'application en Release
RUN dotnet publish -c Release -o /app/publish

# ------------------------------
# Étape 2 : Runtime
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copier le build publié
COPY --from=build /app/publish .

# Copier le frontend (important pour servir le SPA)
COPY frontend ./frontend

# Exposer le port utilisé par Render
EXPOSE 10000

# Lancer l'application
ENTRYPOINT ["dotnet", "AttendanceBackend.dll"]
