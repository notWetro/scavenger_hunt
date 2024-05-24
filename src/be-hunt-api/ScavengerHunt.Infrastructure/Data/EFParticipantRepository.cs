using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Infrastructure.Data
{
    public sealed class EFParticipantRepository(ScavHuntDbContext dbContext) : IParticipantRepository
    {
        private readonly ScavHuntDbContext _context = dbContext;

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

        public Task<IEnumerable<Participant>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Participant?> GetByUsernameAsync(string username)
        {
            try
            {
                return await _context.Participants.FirstOrDefaultAsync(p => p.Username == username);
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
