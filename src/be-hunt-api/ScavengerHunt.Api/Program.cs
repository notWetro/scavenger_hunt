using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScavengerHunt.Api.Services;
using ScavengerHunt.Domain.Repositories;
using ScavengerHunt.Infrastructure;
using ScavengerHunt.Infrastructure.Data;
using System.Text;

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
    services.AddScoped<IHuntRepository, EFHuntRepository>()
            .AddScoped<IAssignmentRepository, EFAssignmentRepository>()
            .AddScoped<IParticipationRepository, EFParticipationRepository>()
            .AddScoped<IParticipantRepository, EFParticipantRepository>();

    // Add configuration for JWT
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!);
    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

    services.AddScoped<ITokenService, TokenService>()
            .AddScoped<IAuthenticationService, AuthenticationService>();

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
