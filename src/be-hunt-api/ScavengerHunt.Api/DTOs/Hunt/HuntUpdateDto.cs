namespace ScavengerHunt.Api.DTOs.Hunt
{
    public sealed class HuntUpdateDto
    {
        /// <summary>
        /// Unique identifier of a hunt.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A title usually contains a topic of a hunt.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A description usually contains additional information of a hunt.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
