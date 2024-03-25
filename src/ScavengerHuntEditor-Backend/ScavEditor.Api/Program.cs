using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.Data;

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
    
    // Database-Context (Sql, MariaDb, InMemory, ...)
    services.AddDbContext<ScavEditorApiContext>(options => options.
        UseSqlServer(builder.Configuration.GetConnectionString("ScavEditorApiContext") ?? 
            throw new InvalidOperationException("Connection string 'ScavEditorApiContext' not found.")));

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
