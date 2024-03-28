using ScavengerHunt.Domain.Repositories;
using ScavengerHunt.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;

    // Add services
    services.AddScoped<IHuntRepository, EFHuntRepository>();
    // Add other repositories, DbContext, etc.

    services.AddDbContext<ScavHuntDbContext>(options => options.
        UseSqlServer(builder.Configuration.GetConnectionString("ScavEditorApiContext") ??
            throw new InvalidOperationException("Connection string 'ScavEditorApiContext' not found.")));

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
