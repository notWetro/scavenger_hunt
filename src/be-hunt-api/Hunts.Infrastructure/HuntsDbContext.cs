using Hunts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hunts.Infrastructure
{
    public sealed class HuntsDbContext(DbContextOptions<HuntsDbContext> options) : DbContext(options)
    {
        public DbSet<Hunt> ScavengerHunts { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Assignment>()
                .HasOne(a => a.Hint)
                .WithOne(h => h.Assignment)
                .HasForeignKey<Hint>(h => h.AssignmentId);

            builder.Entity<Assignment>()
                .HasOne(a => a.Solution)
                .WithOne(s => s.Assignment)
                .HasForeignKey<Solution>(s => s.AssignmentId);

            builder.Entity<Assignment>()
                .HasOne(a => a.Hunt)
                .WithMany(h => h.Assignments);

            builder.Entity<Hunt>()
                .HasMany(h => h.Assignments)
                .WithOne(a => a.Hunt)
                .HasForeignKey(a => a.HuntId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
