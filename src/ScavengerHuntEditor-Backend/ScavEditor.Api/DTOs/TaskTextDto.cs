using Newtonsoft.Json;

namespace ScavEditor.Api.DTOs
{
    public sealed class TaskTextDto : TaskDto
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }
}
