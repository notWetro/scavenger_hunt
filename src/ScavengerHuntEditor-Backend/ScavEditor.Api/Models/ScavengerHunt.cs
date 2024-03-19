namespace ScavEditor.Api.Models
{
    public sealed class ScavengerHunt
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public List<Station> Stations { get; set; } = [];
        
        public List<Participation> Participations { get; set; } = [];
    }
}
