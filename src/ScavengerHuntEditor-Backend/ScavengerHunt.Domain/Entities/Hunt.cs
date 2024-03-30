
namespace ScavengerHunt.Domain.Entities
{
    public sealed class Hunt
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // TODO: Ask if aggregation-encapsulation makes sense here.
        // Private: Stattions cannot be added from outside directly but only with AddStation() Method
        public ICollection<Station> Stations { get; set; } = [];
    }
}
