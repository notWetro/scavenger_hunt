# Scavenger-Hunt: Backend

## Introduction

This folder serves as the backend for our scavenger hunts, providing functionality to store, retrieve, and update hunts and participations.
When you use the Hunt-Gui all the following steps become obsolet.

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
            "ParticipantsDbConnection": "Server=localhost;Port=3399;Database=desired_database_name_here;User=root;Password=desired_password_here",
            "ParticipantsCacheConnection": "localhost:6379"
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

## Accessing Backend from another device

If you want to access the backend from your phone for testing purposes, you can do so with the following guide (Windows only).

_Note_: This will only work for devices connected in the same local network.

### Figuring out Local IP-Adress

For accessing a device on the same network, you'll first need to find your local IP-Adress.

1) Open a Terminal Window.
2) Run the command `ipconfig`. This will list your Windows IP configuration.
3) Find the Ethernet Adapter that is connected to your home router. An example is shown below:

```shell
PS C:\Users\xyz> ipconfig

Windows IP Configuration

Ethernet adapter Ethernet X:

   Connection-specific DNS Suffix  . : xxxxx.xxx
   IPv6 Address. . . . . . . . . . . : xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx
   Temporary IPv6 Address. . . . . . : xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx
   Link-local IPv6 Address . . . . . : xxxx::xxxx:xxxx:xxxx:xxxx
   IPv4 Address. . . . . . . . . . . : 192.168.178.69
   Subnet Mask . . . . . . . . . . . : 255.255.255.0
   Default Gateway . . . . . . . . . : xxxx::xxxx:xxxx:xxxx:xxxx
                                       xxx.xxx.xxx.x
```

4) Note down the adress located at `IPv4 Address`.

### Configuring Windows Defender Firewall

1) Open "Windows Defender Firewall with Advanced Security".
2) Right click on "Inbound Rules" and select "New Rule...".
3) For "Type of rule" Select "Port". Click next.
4) Select "TCP" and specify port "5500". Click next.
5) For "Action" select "Allow the connection". Click next.
6) For "When does this rule apply?" choose all. Click next.
7) Give the new rule a name (and optionally a description). Click finish.

With the IP-Adress from [here](#figuring-out-local-ip-adress) and the specified port `5500` you should be able to access your backend from a different device on the same network.

```
http://192.168.178.69:5500/hunts/api/...
http://192.168.178.69:5500/participants/api/...
```