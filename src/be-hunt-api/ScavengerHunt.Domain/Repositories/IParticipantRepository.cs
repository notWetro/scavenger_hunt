using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Domain.Repositories
{
    public interface IParticipantRepository
    {
        /// <summary>
        /// Get a participant.
        /// </summary>
        /// <param name="username">Identifier of requested participant.</param>
        /// <returns>A participant-object if one was found.</returns>
        public Task<Participant?> GetByUsernameAsync(string username);

        /// <summary>
        /// Get all participants.
        /// </summary>
        /// <returns>A list of participants.</returns>
        public Task<IEnumerable<Participant>> GetAll();

        /// <summary>
        /// Saves a new participant.
        /// </summary>
        /// <param name="participant">Participant-object to be saved.</param>
        /// <returns>New identifier of saved participant.</returns>
        public Task<int> AddAsync(Participant participant);

        /// <summary>
        /// Updates an existing participant.
        /// </summary>
        /// <param name="hunt">New participant-object to be saved.</param>
        /// <returns>True if the updating succeeded.</returns>
        public Task<bool> UpdateAsync(Participant participant);

        /// <summary>
        /// Deletes an existing participant.
        /// </summary>
        /// <param name="username">Identifier of requested participant.</param>
        /// <returns>Participant-object that was deleted.</returns>
        public Task<Participant?> DeleteByUsernameAsync(string username);
    }
}
