# Imagem base de runtime para ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002
ENV ASPNETCORE_URLS=http://+:5002

# Imagem de build para compilar e publicar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie apenas o arquivo de projeto (para aproveitar o cache durante o restore)
COPY ["AccountService/AccountService.csproj", "AccountService/"]
RUN dotnet restore "AccountService/AccountService.csproj"

# Copie o restante do código e compile a aplicação
COPY . .
RUN dotnet publish "AccountService/AccountService.csproj" -c Release -o /app/publish --no-restore

# Imagem final de runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AccountService.dll"]
