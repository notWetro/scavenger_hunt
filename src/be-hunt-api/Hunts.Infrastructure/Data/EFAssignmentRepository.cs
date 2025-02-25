using Hunts.Domain.Entities;
using Hunts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hunts.Infrastructure.Data
{
    /// <summary>
    /// Repository für die Verwaltung von Assignment-Entitäten.
    /// </summary>
    public sealed class EFAssignmentRepository(HuntsDbContext dbContext) : IAssignmentRepository
    {
        private readonly HuntsDbContext _context = dbContext;

        /// <summary>
        /// Fügt ein neues Assignment hinzu.
        /// </summary>
        /// <param name="assignment">Das hinzuzufügende Assignment.</param>
        /// <returns>Die ID des hinzugefügten Assignments oder -1 bei Fehler.</returns>
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

        /// <summary>
        /// Löscht ein Assignment anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Assignments.</param>
        /// <returns>Das gelöschte Assignment oder null, wenn nicht gefunden.</returns>
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

        /// <summary>
        /// Holt alle Assignments.
        /// </summary>
        /// <returns>Eine Liste aller Assignments.</returns>
        public async Task<IEnumerable<Assignment>> GetAll()
        {
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

        /// <summary>
        /// Holt ein Assignment anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des Assignments.</param>
        /// <returns>Das Assignment oder null, wenn nicht gefunden.</returns>
        public async Task<Assignment?> GetByIdAsync(int id)
        {
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

        /// <summary>
        /// Aktualisiert ein bestehendes Assignment.
        /// </summary>
        /// <param name="assignment">Das zu aktualisierende Assignment.</param>
        /// <returns>True, wenn erfolgreich, sonst false.</returns>
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
