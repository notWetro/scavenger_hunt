using Participants.Domain.Entities;

namespace Participants.Domain.Repositories
{
    public interface IParticipationRepository
    {
        /// <summary>
        /// Get a participation.
        /// </summary>
        /// <param name="username">Identifier of user.</param>
        /// <returns>A participation list that the user is trying to do.</returns>
        public Task<IEnumerable<Participation>> GetByUsernameAsync(string username);

        /// <summary>
        /// Get a participation.
        /// </summary>
        /// <param name="huntId">Identifier of hunt.</param>
        /// <param name="username">Identifier of user.</param>
        /// <returns>A similar participation that the user is trying to do.</returns>
        public Task<Participation?> GetByIdAndUsernameAsync(int huntId, string username);


        /// <summary>
        /// Get a participation.
        /// </summary>
        /// <param name="id">Identifier of requested participation.</param>
        /// <returns>A participation-object if one was found.</returns>
        public Task<Participation?> GetByIdAsync(int id);

        /// <summary>
        /// Get all participations.
        /// </summary>
        /// <returns>A list of participations.</returns>
        public Task<IEnumerable<Participation>> GetAll();

        /// <summary>
        /// Saves a new participation.
        /// </summary>
        /// <param name="participation">Participation-object to be saved.</param>
        /// <returns>New identifier of saved participation.</returns>
        public Task<int> AddAsync(Participation participation);

        /// <summary>
        /// Updates an existing participation.
        /// </summary>
        /// <param name="hunt">New participation-object to be saved.</param>
        /// <returns>True if the updating succeeded.</returns>
        public Task<bool> UpdateAsync(Participation participation);

        /// <summary>
        /// Deletes an existing participation.
        /// </summary>
        /// <param name="id">Identifier of requested participation.</param>
        /// <returns>Participation-object that was deleted.</returns>
        public Task<Participation?> DeleteByIdAsync(int id);

        public Task<IEnumerable<Participation>?> DeleteMultipleByHuntIdAsync(int huntId);
    }
}
