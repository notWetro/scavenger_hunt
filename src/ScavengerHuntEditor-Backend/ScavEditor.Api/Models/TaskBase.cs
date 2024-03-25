namespace ScavEditor.Api.Models
{
    public abstract class TaskBase
    {
        public int Id { get; set; }

        public int IdStation { get; set; }
        //public Station? Station { get; set; }
    }
}
