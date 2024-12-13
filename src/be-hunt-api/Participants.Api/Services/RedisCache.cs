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

        public async Task<Hunt?> DeleteHuntAsync(int huntId)
        {
            var huntsCache = _muxer.GetDatabase(HUNT_CACHE_INDEX);
            var huntKey = $"{huntId}";
            var huntData = await huntsCache.StringGetDeleteAsync(huntKey);
            return JsonSerializer.Deserialize<Hunt>(huntData!);
        }

        public async Task SaveHuntAsync(Hunt hunt)
        {
            Console.WriteLine($"SaveHuntAsync");
            var huntsCache = _muxer.GetDatabase(HUNT_CACHE_INDEX);
            var huntKey = $"{hunt.Id}";
            var huntData = JsonSerializer.Serialize(hunt);

            await huntsCache.StringSetAsync(huntKey, huntData);
        }

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

        public async Task<Hunt?> GetHuntAsync(int huntId)
        {
            var huntsCache = _muxer.GetDatabase(HUNT_CACHE_INDEX);
            var huntKey = $"{huntId}";

            var huntData = await huntsCache.StringGetAsync(huntKey);
            if (huntData.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<Hunt>(huntData!);
        }

        public async Task<string?> GetUsernameByToken(string token)
        {
            var tokenCache = _muxer.GetDatabase(TOKEN_CACHE_INDEX);
            return await tokenCache.StringGetAsync(token);
        }
    }
}
