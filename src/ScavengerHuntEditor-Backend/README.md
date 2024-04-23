# Scavenger-Hunt: Backend

## Database Setup

To persist our scavenger hunts, we use a SQL database, specifically with MySQL provider. For testing purposes, we utilize container technologies like Docker to easily set up and run a local database that stores dummy data. Below is a guide to setting up the environment for development.

### Installing MySQL

Pull the latest MySQL image from Docker Hub:

```shell
docker pull mysql
```

Run a container using the pulled image:

```shell
docker run --name scavenger-hunt-hsaa -e MYSQL_ROOT_PASSWORD=your_password_here -p 3306:3306 -d mysql:latest
```

### Configuring MySQL

To access the database with our backend, ensure a working connection. In your `appsettings.json`, verify that you have the correct connection string:

```json
"ConnectionStrings": {
    "ScavEditorApiContext": "Server=localhost;Port=3306;Database=ScavengerHuntHsAa;Uid=root;Pwd=your_password_here;"
}
```

Ensure the correct version is specified in the `Program.cs` database configuration. For example, we use Version 8.3.0 of MySQL:

```csharp
services.AddDbContext<ScavHuntDbContext>(options => options
    .EnableSensitiveDataLogging() // Optional, shouldn't be enabled in production
    .UseMySql(
        builder.Configuration.GetConnectionString("ScavEditorApiContext") ?? throw new InvalidOperationException("..."),
        new MySqlServerVersion(new Version(8, 3, 0)),
        b => b.MigrationsAssembly("ScavengerHunt.Infrastructure")));
```

Lastly, update the database migrations accordingly. A migrations file should already exist within the `ScavengerHunt.Infrastructure` assembly. If not, run the `Add-Migration InitialCreate` command in your package manager console. To update the database itself, use the command specified below:

```shell
Update-Database
```

Now, the Backend API can function appropriately.