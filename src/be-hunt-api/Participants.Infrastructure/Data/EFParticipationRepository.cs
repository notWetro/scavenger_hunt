using Microsoft.EntityFrameworkCore;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;

namespace Participants.Infrastructure.Data
{
    /// <summary>
    /// Repository for managing participations using Entity Framework.
    /// </summary>
    public sealed class EFParticipationRepository(ParticipantsDbContext dbContext) : IParticipationRepository
    {
        private readonly ParticipantsDbContext _context = dbContext;

        /// <summary>
        /// Adds a new participation asynchronously.
        /// </summary>
        /// <param name="participation">The participation to add.</param>
        /// <returns>The ID of the added participation.</returns>
        public async Task<int> AddAsync(Participation participation)
        {
            try
            {
                await _context.Participations.AddAsync(participation);
                await _context.SaveChangesAsync();
                return participation.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// Gets a participation by hunt ID and username asynchronously.
        /// </summary>
        /// <param name="huntId">The hunt ID.</param>
        /// <param name="username">The username.</param>
        /// <returns>The participation if found; otherwise, null.</returns>
        public async Task<Participation?> GetByIdAndUsernameAsync(int huntId, string username)
        {
            try
            {
                return await _context.Participations.FirstOrDefaultAsync(p => p.Participant.Username == username && p.HuntId == huntId);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Deletes a participation by ID asynchronously.
        /// </summary>
        /// <param name="id">The participation ID.</param>
        /// <returns>The deleted participation if found; otherwise, null.</returns>
        public Task<Participation?> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all participations asynchronously.
        /// </summary>
        /// <returns>A list of all participations.</returns>
        public async Task<IEnumerable<Participation>> GetAll()
        {
            try
            {
                return await _context.Participations.ToListAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        /// <summary>
        /// Gets a participation by ID asynchronously.
        /// </summary>
        /// <param name="id">The participation ID.</param>
        /// <returns>The participation if found; otherwise, null.</returns>
        public Task<Participation?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a participation asynchronously.
        /// </summary>
        /// <param name="participation">The participation to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(Participation participation)
        {
            try
            {
                var existingParticipation = await _context.Participations.FindAsync(participation.Id);

                if (existingParticipation is not null)
                {
                    _context.Entry(existingParticipation).State = EntityState.Detached;
                }

                _context.Entry(participation).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Gets participations by username asynchronously.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A list of participations for the given username.</returns>
        public async Task<IEnumerable<Participation>> GetByUsernameAsync(string username)
        {
            try
            {
                return await _context.Participations.Where(p => p.Participant.Username == username).ToListAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        /// <summary>
        /// Deletes multiple participations by hunt ID asynchronously.
        /// </summary>
        /// <param name="huntId">The hunt ID.</param>
        /// <returns>A list of participations that were marked as deleted.</returns>
        public async Task<IEnumerable<Participation>?> DeleteMultipleByHuntIdAsync(int huntId)
        {
            try
            {
                var participations = await _context.Participations.Where(p => p.HuntId == huntId).ToListAsync();

                foreach (var participation in participations)
                    participation.Status = ParticipationStatus.Deleted;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return the list of participations that were marked as deleted
                return participations;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        /// <summary>
        /// Marks participations as invalid by hunt ID asynchronously.
        /// </summary>
        /// <param name="huntId">The hunt ID.</param>
        public async Task MakeInvalidByHuntIdAsync(int huntId)
        {
            try
            {
                var participations = await _context.Participations.Where(p => p.HuntId == huntId).ToListAsync();

                foreach (var participation in participations)
                    participation.Status = ParticipationStatus.Invalid;

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }
    }
}
