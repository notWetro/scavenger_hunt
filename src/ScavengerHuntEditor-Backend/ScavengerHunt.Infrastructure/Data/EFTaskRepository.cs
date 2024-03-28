using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Repositories
{
    public sealed class EFTaskRepository : ITaskRepository
    {
        private ScavHuntDbContext _context;

        public EFTaskRepository(ScavHuntDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<TaskBase?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Tasks.FirstOrDefaultAsync(sh => sh.Id == id);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<TaskBase>> GetAll()
        {
            try
            {
                return await _context.Tasks.ToListAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        public async Task<int> AddAsync(TaskBase TaskBase)
        {
            try
            {
                await _context.Tasks.AddAsync(TaskBase);
                _context.SaveChanges();
                return TaskBase.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        public Task<int[]> AddRangeAsync(IEnumerable<TaskBase> TaskBases)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TaskBase TaskBase)
        {
            try
            {
                _context.Entry(TaskBase).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
        public async Task<TaskBase?> DeleteByIdAsync(int id)
        {
            try
            {
                var scavengerTaskBase = await _context.Tasks.FindAsync(id);

                if (scavengerTaskBase is not null)
                {
                    _context.Tasks.Remove(scavengerTaskBase);
                    await _context.SaveChangesAsync();
                    return scavengerTaskBase;
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
