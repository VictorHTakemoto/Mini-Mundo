# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build-env
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Instalar o SDK do .NET para compilar e rodar as ferramentas do EF
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Instalar o dotnet-ef como ferramenta global no container
RUN dotnet tool install --global dotnet-ef

# Atualizar o caminho para incluir as ferramentas do dotnet
ENV PATH="${PATH}:/root/.dotnet/tools"

# Restaurar dependências e rodar o comando de atualização do banco de dados
RUN dotnet restore
RUN dotnet ef database update

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MiniMundo.WebApp/MiniMundo.WebApp.csproj", "MiniMundo.WebApp/"]
COPY ["MiniMundo.DAL/MiniMundo.DAL.csproj", "MiniMundo.DAL/"]
COPY ["MiniMundo.DAL/params.json", "MiniMundo.DAL/"]
COPY ["MiniMundo.DAL/ConnString.json", "MiniMundo.DAL/"]
RUN dotnet restore "./MiniMundo.WebApp/MiniMundo.WebApp.csproj"
COPY . .
WORKDIR "/src/MiniMundo.WebApp"
RUN dotnet build "./MiniMundo.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniMundo.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false