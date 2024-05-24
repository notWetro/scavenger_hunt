using ScavengerHunt.Domain;

namespace ScavengerHunt.Api.DTOs.Hint
{
    public sealed class HintInnerGetDto
    {
        public int Id { get; set; }
        public HintType HintType { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
