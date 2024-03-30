
namespace ScavengerHunt.Domain.Entities
{
    public abstract class TaskBase
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public Station? Station { get; set; }
    }
}
