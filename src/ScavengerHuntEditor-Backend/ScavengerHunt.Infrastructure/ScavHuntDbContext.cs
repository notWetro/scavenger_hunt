using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using System.Reflection.Emit;

namespace ScavengerHunt.Infrastructure
{
    public sealed class ScavHuntDbContext : DbContext
    {
        public DbSet<Hunt> ScavengerHunts { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<TaskText> Tasks { get; set; }

        public ScavHuntDbContext(DbContextOptions<ScavHuntDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskText>();

            builder.Entity<Station>()
                .HasOne(s => s.Hunt)
                .WithMany(h => h.Stations)
                .HasForeignKey(s => s.HuntId)
                .OnDelete(DeleteBehavior.Cascade); // When a hunt is being deleted it also deletes its stations

            builder.Entity<TaskText>()
                .HasOne(t => t.Station)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StationId)
                .OnDelete(DeleteBehavior.Cascade); // When a station is being deleted it also deletes its tasks

            base.OnModelCreating(builder);
        }
    }
}
