# Imagem base de runtime para ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Imagem de build para compilar e publicar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie apenas o arquivo de projeto (para aproveitar o cache durante o restore)
COPY ["BankingSystem.Services.LoanService.csproj", "LoanService/"]
RUN dotnet restore "LoanService/BankingSystem.Services.LoanService.csproj"

# Copie o restante do código e compile a aplicação
WORKDIR "/src/LoanService"
COPY . .
RUN dotnet publish "BankingSystem.Services.LoanService.csproj" -c Release -o /app/publish --no-restore

# Imagem final de runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BankingSystem.Services.LoanService.dll"]
