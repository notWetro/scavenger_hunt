using Participants.Domain.Entities;

namespace Participants.Api.Services
{
    public interface ICache
    {
        /// <summary>
        /// Saves a hunt to the cache.
        /// </summary>
        /// <param name="hunt">The hunt to save.</param>
        Task SaveHuntAsync(Hunt hunt);

        /// <summary>
        /// Retrieves a hunt from the cache.
        /// </summary>
        /// <param name="huntId">The ID of the hunt to retrieve.</param>
        /// <returns>The hunt, or null if not found.</returns>
        Task<Hunt?> GetHuntAsync(int huntId);

        /// <summary>
        /// Updates a hunt in the cache.
        /// </summary>
        /// <param name="huntId">The ID of the hunt to update.</param>
        /// <param name="title">The new title of the hunt.</param>
        /// <param name="assignments">The new assignments of the hunt.</param>
        Task UpdateHuntAsync(int huntId, string title, ICollection<Assignment> assignments);

        /// <summary>
        /// Deletes a hunt from the cache.
        /// </summary>
        /// <param name="huntId">The ID of the hunt to delete.</param>
        /// <returns>The deleted hunt, or null if not found.</returns>
        Task<Hunt?> DeleteHuntAsync(int huntId);

        /// <summary>
        /// Saves a login token to the cache.
        /// </summary>
        /// <param name="participantId">The participant ID associated with the token.</param>
        /// <param name="loginToken">The login token to save.</param>
        Task SaveLoginToken(string participantId, string loginToken);

        /// <summary>
        /// Retrieves a username by token from the cache.
        /// </summary>
        /// <param name="token">The token to search for.</param>
        /// <returns>The username associated with the token, or null if not found.</returns>
        Task<string?> GetUsernameByToken(string token);
    }
}
