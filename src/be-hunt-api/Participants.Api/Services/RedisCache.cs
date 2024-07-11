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

        public Task DeleteHuntAsync(int huntId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveHuntAsync(Hunt hunt)
        {
            var huntsCache = _muxer.GetDatabase(HUNT_CACHE_INDEX);
            var huntKey = $"{hunt.Id}";
            var huntData = JsonSerializer.Serialize(hunt);
            
            await huntsCache.StringSetAsync(huntKey, huntData);
        }

        public async Task SaveLoginToken(string username, string loginToken)
        {
            var tokenCache = _muxer.GetDatabase(TOKEN_CACHE_INDEX);
            await tokenCache.StringSetAsync(username, loginToken);
        }

        public Task UpdateHuntAsync(int huntId, ICollection<int> assignmentIds)
        {
            throw new NotImplementedException();
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
    }
}
