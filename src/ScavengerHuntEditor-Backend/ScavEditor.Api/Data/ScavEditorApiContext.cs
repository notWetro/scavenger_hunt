using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Data
{
    public class ScavEditorApiContext : DbContext
    {
        public ScavEditorApiContext (DbContextOptions<ScavEditorApiContext> options)
            : base(options)
        {
        }

        public DbSet<TaskQuestionAnswer> TaskQuestionAnswer { get; set; } = default!;
        public DbSet<TaskText> TaskText { get; set; } = default!;
        public DbSet<TaskBase> Task { get; set; } = default!;
        public DbSet<Participation> Participation { get; set; } = default!;
        public DbSet<Participant> Participant { get; set; } = default!;
        public DbSet<Station> Station { get; set; } = default!;
        public DbSet<ScavengerHunt> ScavengerHunt { get; set; } = default!;
    }
}
