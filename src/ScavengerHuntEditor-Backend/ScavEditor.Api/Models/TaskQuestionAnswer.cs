namespace ScavEditor.Api.Models
{
    public sealed class TaskQuestionAnswer : TaskBase
    {
        public string Question { get; set; } = string.Empty;
        public string ExpectedAnswer { get; set; } = string.Empty;
    }
}
