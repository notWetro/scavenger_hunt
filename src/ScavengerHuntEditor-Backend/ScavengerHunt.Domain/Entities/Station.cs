namespace ScavengerHunt.Domain.Entities
{
    public sealed class Station
    {
        /// <summary>
        /// Unique identifier of a station.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A name of a station either refers to it's location or a topic.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        // TODO/Note: Instead of Latitude and Longitude we might use Points from
        // NetTopologySuite.Geometries.Point Namespace (for calculating distances, etc.)

        /// <summary>
        /// Geographical horizontal position.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Geographical vertical position.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Each station is associated to a hunt. This refers to the unique identifier of that hunt.
        /// </summary>
        public int HuntId { get; set; }

        /// <summary>
        /// Each station is associated to a hunt. this refers to the concrete object reference of that hunt.
        /// </summary>
        public Hunt? Hunt { get; set; }

        /// <summary>
        /// Each station consists of (multiple) tasks.
        /// </summary>
        public ICollection<TaskText> Tasks { get; set; } = [];
    }
}
