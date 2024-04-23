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

    // Add other repositories, DbContext, etc.

    services.AddDbContext<ScavHuntDbContext>(options => options
    .EnableSensitiveDataLogging()
    .UseMySql(
        builder.Configuration.GetConnectionString("ScavEditorApiContext") ?? throw new InvalidOperationException("Connection string 'ScavEditorApiContext' not found."),
        new MySqlServerVersion(new Version(8, 3, 0)),
    b => b.MigrationsAssembly("ScavengerHunt.Infrastructure")));


    //services.AddDbContext<ScavHuntDbContext>(options => options
    //    .EnableSensitiveDataLogging()
    //    .UseSqlServer(builder.Configuration.GetConnectionString("ScavEditorApiContext") ?? throw new InvalidOperationException("Connection string 'ScavEditorApiContext' not found."),
    //    b => b.MigrationsAssembly("ScavengerHunt.Infrastructure")));

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
