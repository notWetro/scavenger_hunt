using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Domain.Repositories
{
    public interface IHuntRepository
    {
        /// <summary>
        /// Get an associated hunt.
        /// </summary>
        /// <param name="id">Identifier of requested hunt.</param>
        /// <returns>A hunt-object if one was found.</returns>
        public Task<Hunt?> GetByIdAsync(int id);

        /// <summary>
        /// Get all hunts.
        /// </summary>
        /// <returns>A list of hunt-objects.</returns>
        public Task<IEnumerable<Hunt>> GetAll();


        /// <summary>
        /// Saves a new hunt.
        /// </summary>
        /// <param name="hunt">Hunt-object to be saved.</param>
        /// <returns>New identifier of saved hunt.</returns>
        public Task<int> AddAsync(Hunt hunt);

        /// <summary>
        /// Updates an existing hunt.
        /// </summary>
        /// <param name="hunt">New hunt-object to be saved.</param>
        /// <returns>True if the updating succeeded.</returns>
        public Task<bool> UpdateAsync(Hunt hunt);

        /// <summary>
        /// Deletes an existing hunt.
        /// </summary>
        /// <param name="id">Identifier of requested hunt.</param>
        /// <returns>Hunt-object that was deleted.</returns>
        public Task<Hunt?> DeleteByIdAsync(int id);
    }
}
