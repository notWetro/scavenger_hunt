using Microsoft.EntityFrameworkCore;
using Participants.Domain.Entities;

namespace Participants.Infrastructure
{
    public sealed class ParticipantsDbContext(DbContextOptions<ParticipantsDbContext> options) : DbContext(options)
    {
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Participation> Participations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Participation>()
                .HasOne(p => p.Participant);

            builder.Entity<Participant>()
                .HasMany(p => p.Participations)
                .WithOne(p => p.Participant)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
