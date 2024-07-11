using Participants.Domain.Entities;

namespace Participants.Api.Services
{
    public interface ICache
    {
        Task SaveHuntAsync(Hunt hunt);
        Task<Hunt?> GetHuntAsync(int huntId);
        Task UpdateHuntAsync(int huntId, ICollection<int> assignmentIds);
        Task DeleteHuntAsync(int huntId);

        Task SaveLoginToken(string participantId, string loginToken);
    }
}
