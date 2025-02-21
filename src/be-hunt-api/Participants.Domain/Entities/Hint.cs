namespace Participants.Domain.Entities
{
    public sealed class Hint
    {
        /// <summary>
        /// Gets or sets the type of the hint.
        /// </summary>
        public int HintType { get; set; }

        /// <summary>
        /// Gets or sets the data of the hint.
        /// </summary>
        public required string Data { get; set; }

        /// <summary>
        /// Gets or sets the additional data of the hint.
        /// </summary>
        public string? AdditionalData { get; set; }
    }
}
