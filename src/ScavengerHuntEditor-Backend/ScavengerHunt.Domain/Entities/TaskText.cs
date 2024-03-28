namespace ScavengerHunt.Domain.Entities
{
    public sealed class TaskText : TaskBase
    {
        public string Text { get; set; } = string.Empty;
    }
}
