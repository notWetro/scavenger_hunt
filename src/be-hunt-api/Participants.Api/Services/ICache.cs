using Participants.Domain.Entities;

namespace Participants.Api.Services
{
    public interface ICache
    {
        Task SaveHuntAsync(Hunt hunt);
        Task<Hunt?> GetHuntAsync(int huntId);
        Task UpdateHuntAsync(int huntId, ICollection<Assignment> assignments);
        Task<Hunt?> DeleteHuntAsync(int huntId);

        Task SaveLoginToken(string participantId, string loginToken);

        Task<string> GetUsernameByToken(string token);
    }
}
