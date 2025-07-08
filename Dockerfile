# Fase 1: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia EXPLÍCITAMENTE el archivo .csproj (ajusta el nombre)
COPY ["Finanzas_Personales_App.csproj", "."]  # ← Cambia al nombre exacto de tu archivo
RUN dotnet restore "Finanzas_Personales_App.csproj"  # ← Especifica el archivo

# 2. Copia todo el código
COPY . .
RUN dotnet publish "Finanzas_Personales_App.csproj" -c Release -o /app  # ← Especifica el proyecto

# Fase 2: Imagen final para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://*:$PORT
ENTRYPOINT ["dotnet", "Finanzas_Personales_App.dll"]