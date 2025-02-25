namespace Participants.Domain
{
    /// <summary>
    /// Utility class for calculating Levenshtein distance.
    /// </summary>
    public static class LevenshteinUtils
    {
        /// <summary>
        /// Calculates the Levenshtein distance between two strings.
        /// </summary>
        /// <param name="givenText">The given text.</param>
        /// <param name="expectedText">The expected text.</param>
        /// <returns>The Levenshtein distance.</returns>
        public static int LevenshteinDistance(string givenText, string expectedText)
        {
            int n = givenText.Length;
            int m = expectedText.Length;
            int[,] d = new int[n + 1, m + 1];

            // Initialize the distance matrix
            for (int i = 0; i <= n; i++)
            {
                d[i, 0] = i;
            }
            for (int j = 0; j <= m; j++)
            {
                d[0, j] = j;
            }

            // Compute the distances
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (givenText[i - 1] == expectedText[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }
    }
}
