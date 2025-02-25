using Hunts.Api;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Adds application-specific services to the IServiceCollection.
/// </summary>
builder.Services.AddApplicationServices();

/// <summary>
/// Configures the database context with the specified settings.
/// </summary
builder.Services.AddDatabaseConfiguration(builder.Configuration);

/// <summary>
/// Configures CORS settings for the application.
/// </summary>
builder.Services.AddCorsConfiguration();

builder.WebHost.ConfigureKestrel(options =>
{
    /// <summary>
    /// Sets the maximum request body size to 100 MB.
    /// </summary>
    options.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowSpecificOrigin");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
