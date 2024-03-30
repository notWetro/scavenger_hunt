using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Domain.Repositories
{
    public interface IStationRepository
    {
        /// <summary>
        /// Get an associated station.
        /// </summary>
        /// <param name="id">Identifier of requested station.</param>
        /// <returns>A station-object if one was found.</returns>
        public Task<Station?> GetByIdAsync(int id);

        /// <summary>
        /// Get all stations.
        /// </summary>
        /// <returns>A list of station-objects.</returns>
        public Task<IEnumerable<Station>> GetAll();

        /// <summary>
        /// Get all stations of an associated hunt.
        /// </summary>
        /// <param name="id">Identifier of requested hunt.</param>
        /// <returns>A list of station-objects.</returns>
        public Task<IEnumerable<Station>> GetAllByHuntId(int huntId);

        /// <summary>
        /// Saves a new station.
        /// </summary>
        /// <param name="hunt">Station-object to be saved.</param>
        /// <returns>New identifier of saved station.</returns>
        public Task<int> AddAsync(Station station);

        /// <summary>
        /// Updates an existing station.
        /// </summary>
        /// <param name="hunt">New station-object to be saved.</param>
        /// <returns>True if the updating succeeded.</returns>
        public Task<bool> UpdateAsync(Station station);

        /// <summary>
        /// Deletes an existing station.
        /// </summary>
        /// <param name="id">Identifier of requested station.</param>
        /// <returns>Station-object that was deleted.</returns>
        public Task<Station?> DeleteByIdAsync(int id);
    }
}
