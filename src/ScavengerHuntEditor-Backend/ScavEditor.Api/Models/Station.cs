namespace ScavEditor.Api.Models
{
    public sealed class Station
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Note: Instead of Latitude and Longitude we might use Points from
        // NetTopologySuite.Geometries.Point Namespace (for calculating distances, etc.)

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public List<TaskBase> Tasks { get; set; } = [];

        public int IdScavengerHunt { get; set; }
        //public ScavengerHunt? AssociatedScavengerHunt { get; set; }
    }
}
