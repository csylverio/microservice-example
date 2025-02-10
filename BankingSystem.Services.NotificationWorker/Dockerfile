# Use a imagem oficial do SDK do .NET 8 para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos .csproj e restaure as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie o restante dos arquivos do projeto e compile
COPY . ./
RUN dotnet publish -c Release -o out

# Use a imagem do runtime do .NET 8 para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Defina o comando para executar o Worker Service
ENTRYPOINT ["dotnet", "NotificationService.dll"]