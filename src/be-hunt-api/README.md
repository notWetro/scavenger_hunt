# Scavenger-Hunt: Backend

## Introduction

This project serves as the backend for our scavenger hunts, providing functionality to store, retrieve, and update hunts and participations.

## Getting Started with Docker-Compose

To run the backend using Docker-Compose, follow these setup instructions.

### Configuration Files

Ensure the following configuration files are in place:

1. **.env File**: This file should be in the root directory of the `hunt-api` and contain:

    ```shell
    HUNTS_DB_NAME=desired_database_name_here
    HUNTS_DB_ROOT_PWD=desired_password_here
    PARTICIPANTS_DB_NAME=desired_database_name_here
    PARTICIPANTS_DB_ROOT_PWD=desired_password_here
    ```

2. **appsettings.json Files**: These files should be located in the corresponding Api directory (`Hunts.Api` or `Participants.Api`) and contain the appropriate connection strings. Each complete file is defined below:

    1. **appsettings.json** for `Hunts.Api`:

    ```json
    {
        "ConnectionStrings": {
            "HuntsDbConnection": "Server=localhost;Port=3366;Database=desired_database_name_here;User=root;Password=desired_password_here"
        },
        "RabbitMQHost": "localhost",
        "RabbitMQPort":  5672,
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
            }
        },
        "AllowedHosts": "*"
    }
    ```

    2. **appsettings.json** for `Participations.Api`:

    ```json
    {
        "ConnectionStrings": {
            "ParticipantsDbConnection": "Server=localhost;Port=3399;Database=desired_database_name_here;User=root;Password=desired_password_here"
        },
        "RabbitMQHost": "localhost",
        "RabbitMQPort":  5672,
        "Jwt": {
            "Key": "Xx69420-EncryptionIsGood-69420xX",
            "Issuer": "YourIssuer",
            "Audience": "YourAudience"
        },
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
            }
        },
        "AllowedHosts": "*"
    }
    ```

### Running the Application

After setting up the configuration files, build and run the backend with the following command executed from the root directory of the `hunt-api`:

```shell
docker-compose up --build
```

_Note_: Ensure the Docker Daemon is running before executing any Docker commands.

## Database Setup for Development

We use a MySQL database to persist scavenger hunts. For development and testing, we utilize Docker to set up a local MySQL database. Follow these steps to configure your environment.

### Pulling MySQL Image

Pull the latest MySQL image from Docker Hub:

```shell
docker pull mysql
```

### Running MySQL Container

Run a container with the pulled MySQL image:

```shell
docker run --name scavenger-hunt-db -e MYSQL_ROOT_PASSWORD=desired_password_here -p 3306:3306 -d mysql:latest
```

For running each database individually use the following commands:

```shell
docker run --name scavenger-hunts-db -e MYSQL_ROOT_PASSWORD=desired_password_here -p 3366:3306 -d mysql:latest

docker run --name scavenger-participations-db -e MYSQL_ROOT_PASSWORD=desired_password_here -p 3399:3306 -d mysql:latest
```

### Running migrations on MySQL Containers

Ensure your database-migrations are up to date. If the initial migrations don't exist, create them:

```shell
dotnet ef migrations add InitialCreate --startup-project ..\Hunts.Api\ --project .
dotnet ef migrations add InitialCreate --startup-project ..\Participants.Api\ --project .
```

Now, you can apply the database-migrations:

```shell
dotnet ef database update --startup-project ..\Hunts.Api\ --project .
dotnet ef database update --startup-project ..\Participants.Api\ --project .
```

By following these steps, your development environment should be correctly set up for running and testing the backend of the scavenger hunt application.