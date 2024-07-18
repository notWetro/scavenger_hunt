using Hunts.Api.Services;
using Hunts.Domain.Repositories;
using Hunts.Infrastructure;
using Hunts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hunts.Api
{
    public static class ServiceExtensions
    {
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

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            return services;
        }
    }
}
