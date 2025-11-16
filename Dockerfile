# Étape 1 : builder
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copier les fichiers csproj et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier le reste des fichiers et publier
COPY . ./
RUN dotnet publish -c Release -o out

# Étape 2 : runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Exposer le port sur lequel l'app va tourner
EXPOSE 5000

# Démarrer l'application
ENTRYPOINT ["dotnet", "AttendanceBackend.dll"]
