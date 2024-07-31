using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Participants.Api.Services;
using Participants.Domain.Repositories;
using Participants.Infrastructure;
using Participants.Infrastructure.Data;
using StackExchange.Redis;
using System.Text;

namespace Participants.Api
{
    public static class ServiceExtensions
    {
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

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("http://localhost:5173")
                        .WithOrigins("http://localhost:4173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            return services;
        }
    }
}
