using Participants.Api;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Adds application services to the service collection.
/// </summary>
builder.Services.AddApplicationServices(builder.Configuration);

/// <summary>
/// Adds JWT configuration to the service collection.
/// </summary>
builder.Services.AddJwtConfiguration(builder.Configuration);

/// <summary>
/// Adds database configuration to the service collection.
/// </summary>
builder.Services.AddDatabaseConfiguration(builder.Configuration);

/// <summary>
/// Adds CORS configuration to the service collection.
/// </summary>
builder.Services.AddCorsConfiguration();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
});

// Add services to the container.

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

