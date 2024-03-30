using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Repositories;
using ScavengerHunt.Infrastructure;
using ScavengerStation.Domain.Repositories;

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
    services.AddScoped<IHuntRepository, EFHuntRepository>();
    services.AddScoped<IStationRepository, EFStationRepository>();
    services.AddScoped<ITaskRepository, EFTaskRepository>();

    // Add other repositories, DbContext, etc.

    services.AddDbContext<ScavHuntDbContext>(options => options
        .EnableSensitiveDataLogging()
        .UseSqlServer(builder.Configuration.GetConnectionString("ScavEditorApiContext") ?? throw new InvalidOperationException("Connection string 'ScavEditorApiContext' not found."),
        b => b.MigrationsAssembly("ScavengerHunt.Infrastructure")));

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
