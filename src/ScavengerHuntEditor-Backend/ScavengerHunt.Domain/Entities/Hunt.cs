
namespace ScavengerHunt.Domain.Entities
{
    public sealed class Hunt
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // TODO: Ask if aggregation-encapsulation makes sense here.
        // Private: Stattions cannot be added from outside directly but only with AddStation() Method
        private readonly List<Station> _stations;
        public IReadOnlyCollection<Station> Stations => _stations.AsReadOnly();

        // TODO: Ask if this is needed. _isDraft flag would allow for hunts to be "private" until the needed changes have been made
        public bool _isDraft;
        public static Hunt NewHunt() => new() { _isDraft = true };

        public Hunt()
        {
            _isDraft = false;
            _stations = [];
        }

        public void AddStation(Station station) => _stations.Add(station);
    }
}
