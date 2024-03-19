using Newtonsoft.Json;

namespace ScavEditor.Api.DTOs
{
    public sealed class TaskQuestionAnswerDto : TaskDto
    {
        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;

        //[JsonProperty("expectedAnswer")]
        //public string ExpectedAnswer { get; set; } = string.Empty;
    }
}
