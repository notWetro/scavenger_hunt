using Participants.Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Participants.Api.Services
{
    public sealed class RedisCache(IConnectionMultiplexer muxer) : ICache
    {
        private readonly IConnectionMultiplexer _muxer = muxer;
        private const int HUNT_CACHE_INDEX = 0;
        private const int TOKEN_CACHE_INDEX = 1;

        /// <summary>
        /// Deletes a hunt from the cache.
        /// </summary>
        /// <param name="huntId">The ID of the hunt to delete.</param>
        /// <returns>The deleted hunt, or null if not found.</returns>
        public async Task<Hunt?> DeleteHuntAsync(int huntId)
        {
            var huntsCache = _muxer.GetDatabase(HUNT_CACHE_INDEX);
            var huntKey = $"{huntId}";
            var huntData = await huntsCache.StringGetDeleteAsync(huntKey);
            return JsonSerializer.Deserialize<Hunt>(huntData!);
        }

        /// <summary>
        /// Saves a hunt to the cache.
        /// </summary>
        /// <param name="hunt">The hunt to save.</param>
        public async Task SaveHuntAsync(Hunt hunt)
        {
            var huntsCache = _muxer.GetDatabase(HUNT_CACHE_INDEX);
            var huntKey = $"{hunt.Id}";
            var huntData = JsonSerializer.Serialize(hunt);

            await huntsCache.StringSetAsync(huntKey, huntData);
        }

        /// <summary>
        /// Saves a login token to the cache.
        /// </summary>
        /// <param name="username">The username associated with the token.</param>
        /// <param name="loginToken">The login token to save.</param>
        public async Task SaveLoginToken(string username, string loginToken)
        {
            var tokenCache = _muxer.GetDatabase(TOKEN_CACHE_INDEX);
            var tasks = new Task[]
            {
                    tokenCache.StringSetAsync(username, loginToken),
                    tokenCache.StringSetAsync(loginToken, username)
            };
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Updates a hunt in the cache.
        /// </summary>
        /// <param name="huntId">The ID of the hunt to update.</param>
        /// <param name="title">The new title of the hunt.</param>
        /// <param name="assignments">The new assignments of the hunt.</param>
        public async Task UpdateHuntAsync(int huntId, string title, ICollection<Assignment> assignments)
        {
            var hunt = await this.DeleteHuntAsync(huntId);

            if (hunt is not null)
            {
                hunt.Title = title;
                hunt.Assignments = assignments;
                await this.SaveHuntAsync(hunt);
            }
        }

        /// <summary>
        /// Retrieves a hunt from the cache.
        /// </summary>
        /// <param name="huntId">The ID of the hunt to retrieve.</param>
        /// <returns>The hunt, or null if not found.</returns>
        public async Task<Hunt?> GetHuntAsync(int huntId)
        {
            var huntsCache = _muxer.GetDatabase(HUNT_CACHE_INDEX);
            var huntKey = $"{huntId}";

            var huntData = await huntsCache.StringGetAsync(huntKey);
            if (huntData.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<Hunt>(huntData!);
        }

        /// <summary>
        /// Retrieves a username by token from the cache.
        /// </summary>
        /// <param name="token">The token to search for.</param>
        /// <returns>The username associated with the token, or null if not found.</returns>
        public async Task<string?> GetUsernameByToken(string token)
        {
            var tokenCache = _muxer.GetDatabase(TOKEN_CACHE_INDEX);
            return await tokenCache.StringGetAsync(token);
        }
    }
}
