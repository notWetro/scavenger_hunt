using Hunts.Domain.Entities;

namespace Hunts.Domain.Repositories
{
    /// <summary>
    /// Interface for accessing assignment data.
    /// </summary>
    public interface IAssignmentRepository
    {
        /// <summary>
        /// Get an assignment.
        /// </summary>
        /// <param name="id">Identifier of requested assignment.</param>
        /// <returns>An assignment-object if one was found.</returns>
        public Task<Assignment?> GetByIdAsync(int id);

        /// <summary>
        /// Get all assignments.
        /// </summary>
        /// <returns>A list of assignments.</returns>
        public Task<IEnumerable<Assignment>> GetAll();

        /// <summary>
        /// Saves a new assignment.
        /// </summary>
        /// <param name="assignment">Assignment-object to be saved.</param>
        /// <returns>New identifier of saved assignment.</returns>
        public Task<int> AddAsync(Assignment assignment);

        /// <summary>
        /// Updates an existing assignment.
        /// </summary>
        /// <param name="hunt">New assignment-object to be saved.</param>
        /// <returns>True if the updating succeeded.</returns>
        public Task<bool> UpdateAsync(Assignment assignment);

        /// <summary>
        /// Deletes an existing assignment.
        /// </summary>
        /// <param name="id">Identifier of requested assignment.</param>
        /// <returns>Assignment-object that was deleted.</returns>
        public Task<Assignment?> DeleteByIdAsync(int id);
    }
}
