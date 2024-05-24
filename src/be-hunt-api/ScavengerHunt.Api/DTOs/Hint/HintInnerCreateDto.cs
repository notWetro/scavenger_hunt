using ScavengerHunt.Domain;

namespace ScavengerHunt.Api.DTOs.Hint
{
    public sealed class HintInnerCreateDto
    {
        public HintType HintType { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
