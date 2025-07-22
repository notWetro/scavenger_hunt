using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Participants.Api.Services;
using Participants.Domain.Repositories;
using Participants.Infrastructure;
using Participants.Infrastructure.Data;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace Participants.Api
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds application services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IEventProcessor, EventProcessor>()
                    .AddScoped<IParticipantRepository, EFParticipantRepository>()
                    .AddScoped<IParticipationRepository, EFParticipationRepository>();

            var cacheConnectionString =
                configuration.GetConnectionString("ParticipantsCacheConnection") ?? throw new Exception("Missing string 'ParticipantsCacheConnection'.");
            //cacheConnectionString += ",AbortOnConnectFail=false;";
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(cacheConnectionString))
                    .AddSingleton<ICache, RedisCache>();

            services.AddHostedService<MessageBusSubscriber>();

            services.AddScoped<ITokenService, TokenService>()
                    .AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        /// <summary>
        /// Adds JWT configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return services;
        }

        /// <summary>
        /// Adds database configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionString =
                configuration.GetConnectionString("ParticipantsDbConnection") ?? throw new Exception("Missing string 'ParticipantsDbConnection'.");

            services.AddDbContext<ParticipantsDbContext>(options => options
                .EnableSensitiveDataLogging()
                .UseMySql(databaseConnectionString, new MySqlServerVersion(new Version(8, 3, 0)), b => b.MigrationsAssembly("Participants.Infrastructure")
                .EnableRetryOnFailure()));

            return services;
        }

        /// <summary>
        /// Adds CORS configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
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

		/// <summary>
		/// Represents an IP address.
		/// </summary>
		public class IpAdress
		{
			public string? Ip { get; set; }
		}
	}
}
