# Use a imagem oficial do runtime ASP.NET Core 8.0 como base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Use a imagem oficial do SDK do .NET 8 para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie os arquivos do projeto e restaure as dependências
COPY . .
RUN dotnet restore "AccountService/AccountService.csproj"

# Compile a aplicação em modo Release
RUN dotnet build "AccountService/AccountService.csproj" -c Release -o /app/build

# Publique a aplicação em um diretório para execução final
FROM build AS publish
RUN dotnet publish "AccountService/AccountService.csproj" -c Release -o /app/publish

# Use a imagem base novamente para criar a imagem final de runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountService.dll"]
