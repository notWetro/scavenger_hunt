using Hunts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hunts.Infrastructure
{
    
    public sealed class HuntsDbContext(DbContextOptions<HuntsDbContext> options) : DbContext(options)
    {
        public required DbSet<Hunt> ScavengerHunts { get; set; }
        public required DbSet<Assignment> Assignments { get; set; }

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

            builder.Entity<Hint>()
            .Property(h => h.additionalData)
            .HasColumnType("text")
            .IsRequired(false);
           
            base.OnModelCreating(builder);
        }
    }
}
