using Hunts.Domain.Repositories;
using Hunts.Infrastructure;
using Hunts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;

    // Add services
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddScoped<IHuntRepository, EFHuntRepository>()
            .AddScoped<IAssignmentRepository, EFAssignmentRepository>();

    services.AddDbContext<HuntsDbContext>(options => options
                .EnableSensitiveDataLogging()
                .UseMySql(
                    builder.Configuration.GetConnectionString("HuntsDbConnection") ?? throw new Exception("Connection string 'HuntsDbConnection' not found."),
                    new MySqlServerVersion(new Version(8, 3, 0)), b => b.MigrationsAssembly("Hunts.Infrastructure")
                .EnableRetryOnFailure()));

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Add cors policies
    services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder
                .WithOrigins("http://localhost:5173") // Add the specific origins you want to allow
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}
