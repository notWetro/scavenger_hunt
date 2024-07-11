ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM ${DOTNET_RUNTIME} AS base
ENV ASPNETCORE_URLS=http://+:7105
WORKDIR /home/app
EXPOSE 7105

# Base for build
FROM ${DOTNET_SDK} AS buildbase
WORKDIR /source

COPY . .

# # Copy solution files and project files
# COPY ["Backend-Hunt-Api.sln", "./"]
# COPY ["Hunts.Api/Hunts.Api.csproj", "Hunts.Api/Hunts.Api.csproj"]
# COPY ["Hunts.Domain/Hunts.Domain.csproj", "Hunts.Domain/Hunts.Domain.csproj"]
# COPY ["Hunts.Infrastructure/Hunts.Infrastructure.csproj", "Hunts.Infrastructure/Hunts.Infrastructure.csproj"]
# COPY ["Participants.Api/Participants.Api.csproj", "Participants.Api/Participants.Api.csproj"]
# COPY ["Participants.Domain/Participants.Domain.csproj", "Participants.Domain/Participants.Domain.csproj"]
# COPY ["Participants.Infrastructure/Participants.Infrastructure.csproj", "Participants.Infrastructure/Participants.Infrastructure.csproj"]

# Restore solution structure
RUN dotnet restore Backend-Hunt-Api.sln

# # Copy missing source files
# COPY ["Hunts.Api/", "Hunts.Api/"]
# COPY ["Hunts.Domain/", "Hunts.Domain/"]
# COPY ["Hunts.Infrastructure/", "Hunts.Infrastructure/"]
# COPY ["Participants.Api/", "Participants.Api/"]
# COPY ["Participants.Domain/", "Participants.Domain/"]
# COPY ["Participants.Infrastructure/", "Participants.Infrastructure/"]

# Run ef-core database migrations
FROM buildbase as migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project Participants.Infrastructure/ --startup-project Participants.Api/
