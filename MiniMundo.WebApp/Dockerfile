# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Fase base para execução no modo rápido (padrão para depuração no VS)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base-env
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de build e instalação do SDK e ferramentas necessárias
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Instalar o SDK do .NET para compilar e rodar as ferramentas do EF
RUN dotnet tool install --global dotnet-ef

# Atualizar o caminho para incluir as ferramentas do dotnet
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiar e restaurar dependências
WORKDIR /src
COPY ["MiniMundo.WebApp/MiniMundo.WebApp.csproj", "MiniMundo.WebApp/"]
COPY ["MiniMundo.DAL/MiniMundo.DAL.csproj", "MiniMundo.DAL/"]
COPY ["MiniMundo.API/MiniMundo.API.csproj", "MiniMundo.API/"]
RUN dotnet restore "./MiniMundo.WebApp/MiniMundo.WebApp.csproj"

# Rodar o comando de atualização do banco de dados (migrations)
RUN dotnet ef database update

# Copiar o restante dos arquivos para o container e compilar o projeto
COPY . .
WORKDIR "/src/MiniMundo.WebApp"
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "./MiniMundo.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar a aplicação
FROM build-env AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniMundo.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final para rodar a aplicação
FROM base-env AS final
WORKDIR /app

# Copiar os artefatos publicados
COPY --from=publish /app/publish .

# Adiciona o caminho das ferramentas do .NET ao PATH
RUN echo 'export PATH="$PATH:/root/.dotnet/tools"' >> ~/.bashrc

# Comando de entrada para rodar a aplicação
ENTRYPOINT ["dotnet", "MiniMundo.WebApp.dll"]
