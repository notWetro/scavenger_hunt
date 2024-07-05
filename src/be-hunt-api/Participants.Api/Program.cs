using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Participants.Api.Services;
using Participants.Domain.Repositories;
using Participants.Infrastructure;
using Participants.Infrastructure.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

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

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSingleton<IEventProcessor, EventProcessor>()
            .AddScoped<IParticipantRepository, EFParticipantRepository>()
            .AddScoped<IParticipationRepository, EFParticipationRepository>();

    services.AddDbContext<ParticipantsDbContext>(options => options
            .EnableSensitiveDataLogging()
            .UseMySql(
                builder.Configuration.GetConnectionString("ParticipantsDbConnection") ?? throw new Exception("Connection string 'ParticipantsDbConnection' not found."),
                new MySqlServerVersion(new Version(8, 3, 0)), b => b.MigrationsAssembly("Participants.Infrastructure")
            .EnableRetryOnFailure()));

    services.AddHostedService<MessageBusSubscriber>();

    services.AddScoped<ITokenService, TokenService>()
            .AddScoped<IAuthenticationService, AuthenticationService>();

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

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

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