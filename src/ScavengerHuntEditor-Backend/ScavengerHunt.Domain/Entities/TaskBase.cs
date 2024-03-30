
namespace ScavengerHunt.Domain.Entities
{
    public abstract class TaskBase
    {
        /// <summary>
        /// Unique identifier of a task.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Each task is associated to a station. This refers to the unique identifier of that station.
        /// </summary>
        public int StationId { get; set; }

        /// <summary>
        /// Each task is associated to a station. this refers to the concrete object reference of that station.
        /// </summary>
        public Station? Station { get; set; }
    }
}
