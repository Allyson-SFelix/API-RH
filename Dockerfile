# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos da solução
COPY . .

# Restaura os pacotes da solução
RUN dotnet restore "API_ARMAZENA_FUNCIONARIOS.sln"

# Publica o projeto em Release
RUN dotnet publish "API_ARMAZENA_FUNCIONARIOS.sln" -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Inicia a aplicação
ENTRYPOINT ["dotnet", "API_ARMAZENA_FUNCIONARIOS.dll"]
