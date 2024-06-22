using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Infrastructure.Data
{
    public sealed class EFParticipationRepository(ScavHuntDbContext dbContext) : IParticipationRepository
    {
        private readonly ScavHuntDbContext _context = dbContext;

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

        public async Task<Participation?> GetLatestByUsernameAsync(string username)
        {
            try
            {
                return await _context.Participations
                             .Where(p => p.Participant.Username == username)
                             .Include(p => p.CurrentAssignment)
                                .ThenInclude(a => a.Hint)
                             .Include(p => p.CurrentAssignment)
                                .ThenInclude(a => a.Solution)
                             .OrderByDescending(p => p.Id)
                             .FirstOrDefaultAsync();
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

        public Task<IEnumerable<Participation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Participation?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Participation participation)
        {
            throw new NotImplementedException();
        }
    }
}
