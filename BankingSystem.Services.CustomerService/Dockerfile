# Imagem base de runtime para .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Imagem de build para compilar a aplicação com o SDK do .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "CustomerService/CustomerService.csproj"
RUN dotnet build "CustomerService/CustomerService.csproj" -c Release -o /app/build

# Etapa de publicação dos binários
FROM build AS publish
RUN dotnet publish "CustomerService/CustomerService.csproj" -c Release -o /app/publish

# Imagem final de runtime para executar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerService.dll"]