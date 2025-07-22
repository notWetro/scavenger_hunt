namespace Participants.Api.Services
{
    public static class AuthenticationHelper
    {
        /// <summary>
        /// Validates a token and retrieves the associated username if the token is valid.
        /// </summary>
        /// <param name="cache">The cache to check for the token.</param>
        /// <param name="token">The token to validate.</param>
        /// <returns>The username if the token is valid, otherwise null.</returns>
        public static async Task<string?> GetUsernameIfValidAsync(ICache cache, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            return await cache.GetUsernameByToken(token);
        }
    }
}
