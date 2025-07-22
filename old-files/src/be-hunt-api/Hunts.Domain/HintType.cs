namespace Hunts.Domain
{
    /// <summary>
    /// Represents the type of hint for an assignment.
    /// </summary>
    public enum HintType
    {
        /// <summary>
        /// Hint is a text.
        /// </summary>
        Text,

        /// <summary>
        /// Hint is an image.
        /// </summary>
        Image,

        /// <summary>
        /// Hint is a video.
        /// </summary>
        Video,

        /// <summary>
        /// Hint is an audio.
        /// </summary>
        Audio,

        /// <summary>
        /// Hint is an object.
        /// </summary>
        Object,
    }
}
