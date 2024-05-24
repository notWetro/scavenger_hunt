using ScavengerHunt.Api.DTOs.Assignment;

namespace ScavengerHunt.Api.DTOs.Hunt
{
    public sealed class HuntCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<AssignmentInnerCreateDto> Assignments { get; set; } = [];
    }
}
