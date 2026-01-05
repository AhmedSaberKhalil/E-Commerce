# Stage 1: Runtime + SQL Tools
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
RUN apt-get update && apt-get install -y curl gnupg \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/debian/12/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y mssql-tools18 unixodbc-dev \
    && apt-get clean && rm -rf /var/lib/apt/lists/*
ENV PATH="$PATH:/opt/mssql-tools18/bin"
WORKDIR /app

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "E-CommerceWebApi/E-CommerceWebApi.csproj"

# Stage 3: Publish 
FROM build AS publish
RUN dotnet publish "E-CommerceWebApi/E-CommerceWebApi.csproj" -c Release -o /app/publish

# Stage 4: Final Production Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy Migration Scripts
COPY E-CommerceWebApi/migrate.sh .
COPY E-CommerceWebApi/migrate.sql .
RUN chmod +x migrate.sh

ENTRYPOINT ["dotnet", "E-CommerceWebApi.dll"]
