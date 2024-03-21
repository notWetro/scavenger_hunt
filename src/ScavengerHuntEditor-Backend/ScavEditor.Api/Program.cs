using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScavEditor.Api;
using ScavEditor.Api.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

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

static void ConfigureServices(IServiceCollection services)
{
    // Database-Context (Sql, MariaDb, InMemory, ...)
    services.AddDbContext<ScavEditorApiContext>(options => options.UseInMemoryDatabase("Hoes"));
    // UseSqlServer(builder.Configuration.GetConnectionString("ScavEditorApiContext") ?? throw new InvalidOperationException("Connection string 'ScavEditorApiContext' not found.")));

    // Configure Auto-Mapper (used inside Controllers)
    ConfigureAutoMapper(services);

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

static void ConfigureAutoMapper(IServiceCollection services)
{
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new MappingProfile());
    });

    IMapper mapper = mappingConfig.CreateMapper();
    services.AddSingleton(mapper);
}