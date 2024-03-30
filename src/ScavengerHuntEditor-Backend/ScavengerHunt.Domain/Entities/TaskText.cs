namespace ScavengerHunt.Domain.Entities
{
    public sealed class TaskText : TaskBase
    {
        /// <summary>
        /// This type of task contains a describing text. It gives an action to the user to fulfill.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
