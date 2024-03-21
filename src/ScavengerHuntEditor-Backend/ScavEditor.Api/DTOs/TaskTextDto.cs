using Newtonsoft.Json;

namespace ScavEditor.Api.DTOs
{
    public sealed class TaskTextDto : TaskBaseDto
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }
}
