namespace ScavengerHunt.Domain.Entities
{
    public sealed class Station
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // TODO/Note: Instead of Latitude and Longitude we might use Points from
        // NetTopologySuite.Geometries.Point Namespace (for calculating distances, etc.)

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int HuntId { get; set; }
        public Hunt? Hunt { get; set; }

        public ICollection<TaskText> Tasks { get; set; } = [];
    }
}
