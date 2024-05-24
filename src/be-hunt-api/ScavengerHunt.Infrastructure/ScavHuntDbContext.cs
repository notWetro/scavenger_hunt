using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Infrastructure
{
    public sealed class ScavHuntDbContext(DbContextOptions<ScavHuntDbContext> options) : DbContext(options)
    {
        public DbSet<Hunt> ScavengerHunts { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Participation> Participations { get; set; }

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

            builder.Entity<Participation>()
                .HasOne(p => p.Participant);

            builder.Entity<Participation>()
                .HasOne(h => h.Hunt);

            builder.Entity<Participation>()
                .HasOne(p => p.CurrentAssignment);

            builder.Entity<Hunt>()
                .HasMany(h => h.Assignments)
                .WithOne(a => a.Hunt)
                .HasForeignKey(a => a.HuntId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
