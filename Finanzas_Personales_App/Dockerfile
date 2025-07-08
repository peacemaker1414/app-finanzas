FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Especifica la ruta exacta al .csproj
WORKDIR /src
COPY ["Finanzas_Personales_App/Finanzas_Personales_App.csproj", "Finanzas_Personales_App/"]  # ← Ruta completa

# Cambia al directorio del proyecto
WORKDIR "/src/Finanzas_Personales_App"
RUN dotnet restore "Finanzas_Personales_App.csproj"

# Copia todo el contenido del directorio
COPY . .

# Publica
RUN dotnet publish "Finanzas_Personales_App.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://*:$PORT
ENTRYPOINT ["dotnet", "Finanzas_Personales_App.dll"]