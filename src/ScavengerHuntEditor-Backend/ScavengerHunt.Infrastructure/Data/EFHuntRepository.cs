using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Infrastructure;
using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Infrastructure.Data
{
    public sealed class EFHuntRepository(ScavHuntDbContext dbContext) : IHuntRepository
    {
        private readonly ScavHuntDbContext _context = dbContext;

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
                return await _context.ScavengerHunts
                    .Include(hunt => hunt.Assignments)
                    .ThenInclude(assignment => assignment.Hint)
                    .Include(hunt => hunt.Assignments)
                    .ThenInclude(assignment => assignment.Solution)
                    .ToListAsync();
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

        public async Task<bool> UpdateAsync(Hunt hunt)
        {
            try
            {
                var existingHunt = await _context.ScavengerHunts.FindAsync(hunt.Id);

                if (existingHunt is not null)
                {
                    _context.Entry(existingHunt).State = EntityState.Detached;
                }

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
