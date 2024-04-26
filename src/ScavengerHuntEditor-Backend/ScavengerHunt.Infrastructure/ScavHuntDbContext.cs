using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Infrastructure
{
    public sealed class ScavHuntDbContext(DbContextOptions<ScavHuntDbContext> options) : DbContext(options)
    {
        public DbSet<Hunt> ScavengerHunts { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hunt>()
                .HasMany(h => h.Assignments)
                .WithOne(a => a.Hunt)
                .HasForeignKey(a => a.HuntId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Assignment>()
                .HasOne(a => a.Hint)
                .WithOne(h => h.Assignment)
                .HasForeignKey<Hint>(h => h.AssignmentId);

            builder.Entity<Assignment>()
                .HasOne(a => a.Solution)
                .WithOne(s => s.Assignment)
                .HasForeignKey<Solution>(s => s.AssignmentId);

            //builder.Entity<Assignment>()
            //    .HasOne(s => s.Hunt)
            //    .WithMany(h => h.Stations)
            //    .HasForeignKey(s => s.HuntId)
            //    .OnDelete(DeleteBehavior.Cascade); // When a hunt is being deleted it also deletes its stations

            base.OnModelCreating(builder);
        }
    }
}
