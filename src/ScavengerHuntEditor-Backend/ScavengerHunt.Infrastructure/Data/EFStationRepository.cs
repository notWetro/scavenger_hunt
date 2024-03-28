using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;
using ScavengerHunt.Infrastructure;

namespace ScavengerStation.Domain.Repositories
{
    public sealed class EFStationRepository : IStationRepository
    {
        private ScavHuntDbContext _context;

        // TODO: Logging

        public EFStationRepository(ScavHuntDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Station?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Stations.FirstOrDefaultAsync(sh => sh.Id == id);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<Station>> GetAll()
        {
            try
            {
                return await _context.Stations.ToListAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        public async Task<int> AddAsync(Station Station)
        {
            try
            {
                await _context.Stations.AddAsync(Station);
                _context.SaveChanges();
                return Station.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        public Task<int[]> AddRangeAsync(IEnumerable<Station> Stations)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Station Station)
        {
            try
            {
                _context.Entry(Station).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
        public async Task<Station?> DeleteByIdAsync(int id)
        {
            try
            {
                var scavengerStation = await _context.Stations.FindAsync(id);

                if (scavengerStation is not null)
                {
                    _context.Stations.Remove(scavengerStation);
                    await _context.SaveChangesAsync();
                    return scavengerStation;
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
