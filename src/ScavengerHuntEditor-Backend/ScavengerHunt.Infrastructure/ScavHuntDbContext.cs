using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Infrastructure
{
    public sealed class ScavHuntDbContext : DbContext
    {
        public DbSet<Hunt> ScavengerHunts { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<TaskBase> Tasks { get; set; }

        public ScavHuntDbContext(DbContextOptions<ScavHuntDbContext> options) : base(options) { }
    }
}
