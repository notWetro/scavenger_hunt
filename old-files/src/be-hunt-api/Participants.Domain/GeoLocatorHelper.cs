namespace Participants.Domain
{
    public sealed class GeoLocatorHelper
    {
        private const double RADIUS_OF_EARTH_METERS = 6_371_000;

        /// <summary>
        /// Calculates the distance between two geographical points using the Haversine formula.
        /// </summary>
        /// <param name="lat1">Latitude of the first point.</param>
        /// <param name="lon1">Longitude of the first point.</param>
        /// <param name="lat2">Latitude of the second point.</param>
        /// <param name="lon2">Longitude of the second point.</param>
        /// <returns>Distance in meters.</returns>
        public static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            // Convert degrees to radians
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Distance in meters
            return RADIUS_OF_EARTH_METERS * c;
        }

        /// <summary>
        /// Converts an angle in degrees to radians.
        /// </summary>
        /// <param name="angleInDegrees">Angle in degrees.</param>
        /// <returns>Angle in radians.</returns>
        private static double ToRadians(double angleInDegrees)
        {
            return angleInDegrees * Math.PI / 180.0;
        }
    }
}
