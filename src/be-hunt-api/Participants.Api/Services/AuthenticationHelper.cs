namespace Participants.Api.Services
{
    public static class AuthenticationHelper
    {
        public static async Task<string?> GetUsernameIfValidAsync(ICache cache, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            return await cache.GetUsernameByToken(token);
        }
    }
}
