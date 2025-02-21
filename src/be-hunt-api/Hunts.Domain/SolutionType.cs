namespace Hunts.Domain
{
    /// <summary>
    /// Represents the type of solution for an assignment.
    /// </summary>
    public enum SolutionType
    {
        /// <summary>
        /// Solution is a QR code.
        /// </summary>
        QRCode,

        /// <summary>
        /// Solution is a text.
        /// </summary>
        Text,

        /// <summary>
        /// Solution is a location.
        /// </summary>
        Location
    }
}
