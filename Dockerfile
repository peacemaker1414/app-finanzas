# Fase 1: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos del proyecto y restaura dependencias
COPY *.csproj .
RUN dotnet restore

# Copia todo el código y publica la aplicación
COPY . .
RUN dotnet publish -c Release -o /app

# Fase 2: Imagen final para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia los archivos publicados desde la fase de construcción
COPY --from=build /app .

# Expone el puerto que Render usará (variable PORT se inyectará automáticamente)
ENV ASPNETCORE_URLS=http://*:$PORT
ENTRYPOINT ["dotnet", "Finanzas_Personales_App.dll"]