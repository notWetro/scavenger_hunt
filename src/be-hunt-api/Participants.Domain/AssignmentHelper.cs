using Participants.Domain.Entities;
using Participants.Domain.Repositories;

namespace Participants.Domain
{
    public static class AssignmentHelper
    {
        /// <summary>
        /// Checks if the given solution data is valid for the assignment.
        /// </summary>
        /// <param name="assignment">The assignment to check against.</param>
        /// <param name="givenData">The solution data provided by the user.</param>
        /// <returns>True if the solution is valid, otherwise false.</returns>
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

        /// <summary>
        /// Advances the participation to the next assignment in the hunt.
        /// </summary>
        /// <param name="participationRepository">The participation repository.</param>
        /// <param name="hunt">The hunt containing the assignments.</param>
        /// <param name="participation">The participation to advance.</param>
        /// <param name="currentAssignment">The current assignment.</param>
        public static async Task AdvanceToNextAssignmentAsync(IParticipationRepository participationRepository, Hunt hunt, Participation participation, Assignment currentAssignment)
        {
            var currentAssignmentIndex = hunt.Assignments.ToList().FindIndex(assignment => assignment.Id == currentAssignment.Id);
            if (currentAssignmentIndex < 0 || currentAssignmentIndex >= hunt.Assignments.Count - 1)
            {
                // No more assignments left, mark the hunt as finished
                participation.Status = ParticipationStatus.Finished;
            }
            else
            {
                // Move to the next assignment
                var nextAssignment = hunt.Assignments.ToList()[currentAssignmentIndex + 1];
                participation.CurrentAssignmentId = nextAssignment.Id;
            }

            await participationRepository.UpdateAsync(participation);
        }

        /// <summary>
        /// Gets a hint for the current assignment based on the user's solution data.
        /// </summary>
        /// <param name="userSolutionData">The solution data provided by the user.</param>
        /// <param name="actualSolutionData">The actual solution data.</param>
        /// <param name="actualSolutionType">The type of the actual solution.</param>
        /// <returns>A hint for the current assignment.</returns>
        public static string GetHintForCurrentAssignment(string userSolutionData, string actualSolutionData, int actualSolutionType) => actualSolutionType switch
        {
            1 => $"{DetermineCharacterDifference(userSolutionData, actualSolutionData)}",
            2 => $"{DetermineLocationDistance(userSolutionData, actualSolutionData)}",
            _ => string.Empty,
        };

        /// <summary>
        /// Determines the character difference between the user's solution data and the actual solution data.
        /// </summary>
        /// <param name="userSolutionData">The solution data provided by the user.</param>
        /// <param name="actualSolutionData">The actual solution data.</param>
        /// <returns>The character difference.</returns>
        public static int DetermineCharacterDifference(string userSolutionData, string actualSolutionData) => LevenshteinUtils.LevenshteinDistance(userSolutionData, actualSolutionData);

        /// <summary>
        /// Determines the location distance between the user's solution data and the actual solution data.
        /// </summary>
        /// <param name="userSolutionData">The solution data provided by the user.</param>
        /// <param name="actualSolutionData">The actual solution data.</param>
        /// <returns>The location distance in meters.</returns>
        public static double DetermineLocationDistance(string userSolutionData, string actualSolutionData)
        {
            // Split the input strings to extract latitude and longitude
            var givenCoords = userSolutionData.Split(';');
            var actualCoords = actualSolutionData.Split(';');

            if (givenCoords.Length != 2 || actualCoords.Length != 2)
            {
                throw new ArgumentException("Input data must be in the format 'lat;lon'");
            }

            // Parse latitude and longitude
            double givenLat = double.Parse(givenCoords[0]);
            double givenLon = double.Parse(givenCoords[1]);
            double actualLat = double.Parse(actualCoords[0]);
            double actualLon = double.Parse(actualCoords[1]);

            // Calculate the distance using the Haversine formula
            double distance = GeoLocatorHelper.Haversine(givenLat, givenLon, actualLat, actualLon);

            // Check if the distance is within the specified area
            return distance;
        }

        /// <summary>
        /// Checks if the given location data is inside the specified area.
        /// </summary>
        /// <param name="givenData">The location data provided by the user.</param>
        /// <param name="actualData">The actual location data.</param>
        /// <param name="areaInMeters">The area in meters.</param>
        /// <returns>True if the location is inside the area, otherwise false.</returns>
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
