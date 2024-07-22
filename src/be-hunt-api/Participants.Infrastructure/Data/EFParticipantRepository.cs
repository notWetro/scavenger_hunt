using Microsoft.EntityFrameworkCore;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;

namespace Participants.Infrastructure.Data
{
    public sealed class EFParticipantRepository(ParticipantsDbContext dbContext) : IParticipantRepository
    {
        private readonly ParticipantsDbContext _context = dbContext;

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

        public Task<Participant?> DeleteByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

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

        public Task<bool> UpdateAsync(Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}
