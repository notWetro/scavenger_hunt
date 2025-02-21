using Hunts.Api.Services;
using System.Text.Json;
using Hunts.Domain.Entities;
using Hunts.Domain.Repositories;
using Hunts.Infrastructure;
using Hunts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hunts.Api
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds application-specific services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IMessageBusClient, MessageBusClient>()
                    .AddScoped<IHuntRepository, EFHuntRepository>()
                    .AddScoped<IAssignmentRepository, EFAssignmentRepository>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        /// <summary>
        /// Configures the database context with the specified settings.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <param name="configuration">The configuration to use for database settings.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HuntsDbContext>(options => options
                        .EnableSensitiveDataLogging()
                        .UseMySql(
                            configuration.GetConnectionString("HuntsDbConnection") ?? throw new Exception("Connection string 'HuntsDbConnection' not found."),
                            new MySqlServerVersion(new Version(8, 3, 0)), b => b.MigrationsAssembly("Hunts.Infrastructure")
                        .EnableRetryOnFailure()));

            return services;
        }

        /// <summary>
        /// Configures CORS settings for the application.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            string ipAddress = GetIpAddressFromConfig();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder
                    .WithOrigins(
                        "http://localhost:5173",
                        "http://localhost:4173",
                        "http://scavhunt.local:5173",
                        "http://scavhunt.local:4173",
                        $"http://{ipAddress}:5173",
                        $"http://{ipAddress}:4173"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            return services;
        }

        /// <summary>
        /// Retrieves the IP address from the configuration file.
        /// </summary>
        /// <returns>The IP address as a string.</returns>
        private static string GetIpAddressFromConfig()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "ipconfig.json");
            string json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<IpAdress>(json);

            return config?.Ip ?? "127.0.0.1";
        }

    }
}
