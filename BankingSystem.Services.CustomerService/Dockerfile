# Imagem base de runtime para .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Imagem de build para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas arquivos necessários para o restore
COPY ["BankingSystem.Services.CustomerService.csproj", "CustomerService/"]
RUN dotnet restore "CustomerService/BankingSystem.Services.CustomerService.csproj"

# Copia o restante do código e executa o build e publish em um único comando
WORKDIR "/src/CustomerService"
COPY . .
RUN dotnet publish "BankingSystem.Services.CustomerService.csproj" -c Release -o /app/publish --no-restore

# Imagem final de runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BankingSystem.Services.CustomerService.dll"]
