# Scavenger-Hunt: Backend

## Introduction

This project serves as the backend for our scavenger hunts, providing functionality to store, retrieve, and update hunts and participations.

## Getting Started with Docker-Compose

To run the backend using Docker-Compose, follow these setup instructions.

### Configuration Files

Ensure the following configuration files are in place:

1. **.env File**: This file should be in the root directory of the `hunt-api` and contain:

    ```shell
    MYSQL_ROOT_PASSWORD=desired_password_here
    MYSQL_DATABASE=desired_database_name_here
    ```

2. **appsettings.json File**: This file should be located in the `ScavengerHunt.Api` directory and contain:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=sqlserver;Database=desired_database_name_here;User=root;Password=desired_password_here"
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

### Configuring MySQL for the Backend

Ensure your `appsettings.json` file has the correct connection string to access the database:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=desired_database_name_here;User=root;Password=desired_password_here"
}
```

### Setting Up Entity Framework

Specify the MySQL version in `Program.cs`:

```csharp
services.AddDbContext<ScavHuntDbContext>(options => options
    .EnableSensitiveDataLogging()
    .UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string 'DefaultConnection' not found."),
        new MySqlServerVersion(new Version(8, 3, 0)), 
        b => b.MigrationsAssembly("ScavengerHunt.Infrastructure")
    )
    .EnableRetryOnFailure()
);
```

### Updating the Database

Ensure your database migrations are up to date. If the initial migration does not exist, create it:

```shell
Add-Migration InitialCreate
```

Then, update the database:

```shell
Update-Database
```

By following these steps, your development environment should be correctly set up for running and testing the backend of the scavenger hunt application.