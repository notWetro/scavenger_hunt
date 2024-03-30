
namespace ScavengerHunt.Domain.Entities
{
    public sealed class Hunt
    {
        /// <summary>
        /// Unique identifier of a hunt.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A title usually contains a topic of a hunt.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A description usually contains additional information of a hunt.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Every hunt consists of multiple stations.
        /// TODO: Ask if aggregation-encapsulation makes sense here.
        /// Private: Stattions cannot be added from outside directly but only with AddStation() Method
        /// </summary>
        public ICollection<Station> Stations { get; set; } = [];
    }
}
