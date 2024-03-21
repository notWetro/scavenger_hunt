using Newtonsoft.Json;

namespace ScavEditor.Api.DTOs
{
    public abstract class TaskBaseDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("idStation")]
        public int IdStation { get; set; }

        //[JsonProperty("station")]
        //public StationDto? Station { get; set; }
    }
}
