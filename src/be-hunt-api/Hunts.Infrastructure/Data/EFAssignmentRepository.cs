using Hunts.Domain.Entities;
using Hunts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hunts.Infrastructure.Data
{
    public sealed class EFAssignmentRepository(HuntsDbContext dbContext) : IAssignmentRepository
    {
        private readonly HuntsDbContext _context = dbContext;

        public async Task<int> AddAsync(Assignment assignment)
        {
            try
            {
                await _context.Assignments.AddAsync(assignment);
                await _context.SaveChangesAsync();
                return assignment.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        public async Task<Assignment?> DeleteByIdAsync(int id)
        {
            try
            {
                var assignment = await _context.Assignments.FindAsync(id);

                if (assignment is not null)
                {
                    _context.Assignments.Remove(assignment);
                    await _context.SaveChangesAsync();
                    return assignment;
                }
                return null;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<Assignment>> GetAll()
        {
            Console.WriteLine($"GetAllAssignment");
            try
            {
                return await _context.Assignments
                    .Include(assignment => assignment.Hint)
                    .Include(assignment => assignment.Solution)
                    .Include(assignment => assignment.Hunt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return [];
            }
        }

        public async Task<Assignment?> GetByIdAsync(int id)
        {
            Console.WriteLine($"GetOneIDAssignment");
            try
            {
                return await _context.Assignments.FirstOrDefaultAsync(ass => ass.Id == id);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Assignment assignment)
        {
            try
            {
                var existingAssignment = await _context.Assignments.FindAsync(assignment.Id);

                if (existingAssignment is not null)
                {
                    _context.Entry(existingAssignment).State = EntityState.Detached;
                }

                _context.Entry(assignment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
    }
}
