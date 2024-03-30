namespace ScavengerHunt.Domain.Entities
{
    public sealed class TaskQuestionAnswer : TaskBase
    {
        /// <summary>
        /// This type of task is asking the user a question.
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// This type of task requires a specific answer.
        /// </summary>
        public string ExpectedAnswer { get; set; } = string.Empty;
    }
}
