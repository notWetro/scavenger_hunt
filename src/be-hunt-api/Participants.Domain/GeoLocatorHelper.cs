namespace Participants.Domain
{
    public sealed class GeoLocatorHelper
    {
        private const double RADIUS_OF_EARTH_METERS = 6_371_000;

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

        private static double ToRadians(double angleInDegrees)
        {
            return angleInDegrees * Math.PI / 180.0;
        }
    }
}
