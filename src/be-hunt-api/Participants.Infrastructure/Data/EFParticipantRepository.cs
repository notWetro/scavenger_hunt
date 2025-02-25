using Microsoft.EntityFrameworkCore;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;

namespace Participants.Infrastructure.Data
{
    /// <summary>
    /// Repository for managing participants using Entity Framework.
    /// </summary>
    public sealed class EFParticipantRepository(ParticipantsDbContext dbContext) : IParticipantRepository
    {
        private readonly ParticipantsDbContext _context = dbContext;

        /// <summary>
        /// Adds a new participant asynchronously.
        /// </summary>
        /// <param name="participant">The participant to add.</param>
        /// <returns>The ID of the added participant.</returns>
        public async Task<int> AddAsync(Participant participant)
        {
            try
            {
                await _context.Participants.AddAsync(participant);
                await _context.SaveChangesAsync();
                return participant.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// Deletes a participant by username asynchronously.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The deleted participant if found; otherwise, null.</returns>
        public Task<Participant?> DeleteByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all participants asynchronously.
        /// </summary>
        /// <returns>A list of all participants.</returns>
        public async Task<IEnumerable<Participant>> GetAll()
        {
            try
            {
                return await _context.Participants.ToListAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        /// <summary>
        /// Gets a participant by username asynchronously.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The participant if found; otherwise, null.</returns>
        public async Task<Participant?> GetByUsernameAsync(string username)
        {
            try
            {
                return await _context.Participants.Include(p => p.Participations).FirstAsync(p => p.Username == username);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Updates a participant asynchronously.
        /// </summary>
        /// <param name="participant">The participant to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public Task<bool> UpdateAsync(Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}
