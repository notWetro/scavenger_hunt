using Microsoft.EntityFrameworkCore;

namespace ScavEditor.Api.Data
{
    public class ScavEditorApiContext : DbContext
    {
        public ScavEditorApiContext (DbContextOptions<ScavEditorApiContext> options)
            : base(options)
        {
        }

        public DbSet<DTOs.TaskQuestionAnswerDto> TaskQuestionAnswerDto { get; set; } = default!;
        public DbSet<DTOs.TaskTextDto> TaskTextDto { get; set; } = default!;
        public DbSet<DTOs.TaskDto> TaskDto { get; set; } = default!;
        public DbSet<DTOs.ParticipationDto> ParticipationDto { get; set; } = default!;
        public DbSet<DTOs.ParticipantDto> ParticipantDto { get; set; } = default!;
        public DbSet<DTOs.StationDto> StationDto { get; set; } = default!;
        public DbSet<DTOs.ScavengerHuntDto> ScavengerHuntDto { get; set; } = default!;
    }
}
