using Microsoft.EntityFrameworkCore;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;

namespace Participants.Infrastructure.Data
{
    public sealed class EFParticipationRepository(ParticipantsDbContext dbContext) : IParticipationRepository
    {
        private readonly ParticipantsDbContext _context = dbContext;

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

        public Task<Participation?> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

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

        public Task<Participation?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

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
    }
}
