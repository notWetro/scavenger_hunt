using Participants.Domain.Entities;

namespace Participants.Domain
{
    public static class AssignmentHelper
    {
        public static bool CheckIfValidSolution(Assignment assignment, string givenData)
        {
            var actualData = assignment.Solution.Data;
            var desiredSolution = assignment.Solution.SolutionType;

            switch (desiredSolution)
            {
                case 0:
                    // QR-Code
                    return actualData == givenData;
                case 1:
                    // Text
                    givenData = givenData.Trim().ToLower();
                    actualData = actualData.Trim().ToLower();
                    return givenData == actualData;
                case 2:
                    // Location
                    return IsInsideArea(givenData, actualData, 5);
                default:
                    return false;
            }
        }

        private static bool IsInsideArea(string givenData, string actualData, uint areaInMeters)
        {
            // Split the input strings to extract latitude and longitude
            var givenCoords = givenData.Split(';');
            var actualCoords = actualData.Split(';');

            if (givenCoords.Length != 2 || actualCoords.Length != 2)
            {
                throw new ArgumentException("Input data must be in the format 'lat;lon'");
            }

            // Parse latitude and longitude
            double givenLat = double.Parse(givenCoords[0].Replace('.', ','));
            double givenLon = double.Parse(givenCoords[1].Replace('.', ','));
            double actualLat = double.Parse(actualCoords[0].Replace('.', ','));
            double actualLon = double.Parse(actualCoords[1].Replace('.', ','));

            // Calculate the distance using the Haversine formula
            double distance = GeoLocatorHelper.Haversine(givenLat, givenLon, actualLat, actualLon);

            // Check if the distance is within the specified area
            return distance <= areaInMeters;
        }
    }
}
