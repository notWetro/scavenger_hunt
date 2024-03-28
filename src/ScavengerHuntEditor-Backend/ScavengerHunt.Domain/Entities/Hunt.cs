
namespace ScavengerHunt.Domain.Entities
{
    public sealed class Hunt
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Private, because of aggregation-encapsulation, stations cannot be added from outside directly
        // but only with AddStation()
        private readonly List<Station> _stations;

        public IReadOnlyCollection<Station> Stations => _stations.AsReadOnly();

        public bool _isDraft;

        public static Hunt NewHunt() => new() { _isDraft = true };

        public Hunt()
        {
            _isDraft = false;
            _stations = [];
        }

        public Hunt(int id, string title, string description) : this()
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public void AddStation(Station station) => _stations.Add(station);
    }
}
