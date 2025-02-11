# Etapa de build e publicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie apenas os arquivos .csproj para restaurar as dependências (melhor uso do cache)
COPY *.csproj ./
RUN dotnet restore

# Copie o restante dos arquivos e publique a aplicação em modo Release
COPY . ./
RUN dotnet publish -c Release -o /app/out --no-restore

# Etapa final - imagem menor com apenas o runtime necessário
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Defina o comando para executar o serviço
ENTRYPOINT ["dotnet", "NotificationService.dll"]
