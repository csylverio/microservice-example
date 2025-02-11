FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5003

ENV ASPNETCORE_URLS=http://+:5003

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["BankingService.Services.LoanService/BankingService.Services.LoanService.csproj", "BankingService.Services.LoanService/"]
RUN dotnet restore "BankingService.Services.LoanService/BankingService.Services.LoanService.csproj"
COPY . .
WORKDIR "/src/BankingService.Services.LoanService"
RUN dotnet build "BankingService.Services.LoanService.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BankingService.Services.LoanService.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingService.Services.LoanService.dll"]
