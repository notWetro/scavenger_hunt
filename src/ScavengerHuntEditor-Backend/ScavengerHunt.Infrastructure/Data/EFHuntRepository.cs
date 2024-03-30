using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Infrastructure;

namespace ScavengerHunt.Domain.Repositories
{
    public sealed class EFHuntRepository : IHuntRepository
    {
        private ScavHuntDbContext _context;

        // TODO: Logging

        public EFHuntRepository(ScavHuntDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Hunt?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.ScavengerHunts.FirstOrDefaultAsync(sh => sh.Id == id);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<Hunt>> GetAll()
        {
            try
            {
                return await _context.ScavengerHunts.ToListAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        public async Task<int> AddAsync(Hunt hunt)
        {
            try
            {
                await _context.ScavengerHunts.AddAsync(hunt);
                _context.SaveChanges();
                return hunt.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        public Task<int[]> AddRangeAsync(IEnumerable<Hunt> hunts)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Hunt hunt)
        {
            try
            {
                _context.Entry(hunt).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
        public async Task<Hunt?> DeleteByIdAsync(int id)
        {
            try
            {
                var scavengerHunt = await _context.ScavengerHunts.FindAsync(id);

                if (scavengerHunt is not null)
                {
                    _context.ScavengerHunts.Remove(scavengerHunt);
                    await _context.SaveChangesAsync();
                    return scavengerHunt;
                }
                return null;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }
    }
}
