using Microsoft.EntityFrameworkCore;
using Participants.Domain.Entities;

namespace Participants.Infrastructure
{
    /// <summary>
    /// The database context for participants and participations.
    /// </summary>
    public sealed class ParticipantsDbContext(DbContextOptions<ParticipantsDbContext> options) : DbContext(options)
    {
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Participation> Participations { get; set; }

        /// <summary>
        /// Configures the model relationships and constraints.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Participation>()
                .HasOne(p => p.Participant)
                .WithMany(p => p.Participations);

            builder.Entity<Participant>()
                .HasMany(p => p.Participations)
                .WithOne(p => p.Participant)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
