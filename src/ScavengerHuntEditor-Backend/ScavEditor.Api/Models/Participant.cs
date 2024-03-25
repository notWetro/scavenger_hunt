namespace ScavEditor.Api.Models
{
    public sealed class Participant
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        //public List<Participation> Participations { get; set; } = [];
    }
}
