using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Repositories;
using ScavengerHunt.Infrastructure;
using ScavengerHunt.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;

    // Add services
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddScoped<IHuntRepository, EFHuntRepository>();
    services.AddScoped<IAssignmentRepository, EFAssignmentRepository>();
    services.AddScoped<IParticipationRepository, EFParticipationRepository>();
    services.AddScoped<IParticipantRepository, EFParticipantRepository>();

    // Add other repositories, DbContext, etc.

    services.AddDbContext<ScavHuntDbContext>(options => options
                .EnableSensitiveDataLogging()
                .UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string 'DefaultConnection' not found."),
                    new MySqlServerVersion(new Version(8, 3, 0)), b => b.MigrationsAssembly("ScavengerHunt.Infrastructure")
                .EnableRetryOnFailure()));
    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
