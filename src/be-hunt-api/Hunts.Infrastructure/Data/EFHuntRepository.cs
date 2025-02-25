using Hunts.Domain.Entities;
using Hunts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hunts.Infrastructure.Data
{
    /// <summary>
    /// Repository für die Verwaltung von Hunt-Entitäten.
    /// </summary>
    public sealed class EFHuntRepository(HuntsDbContext dbContext) : IHuntRepository
    {
        private readonly HuntsDbContext _context = dbContext;

        /// <summary>
        /// Holt einen Hunt anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des Hunts.</param>
        /// <returns>Der Hunt oder null, wenn nicht gefunden.</returns>
        public async Task<Hunt?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.ScavengerHunts
                    .Include(hunt => hunt.Assignments)
                        .ThenInclude(assignment => assignment.Hint)
                    .Include(hunt => hunt.Assignments)
                        .ThenInclude(assignment => assignment.Solution)
                    .FirstOrDefaultAsync(sh => sh.Id == id);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Holt alle Hunts.
        /// </summary>
        /// <returns>Eine Liste aller Hunts.</returns>
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

        /// <summary>
        /// Fügt einen neuen Hunt hinzu.
        /// </summary>
        /// <param name="hunt">Der hinzuzufügende Hunt.</param>
        /// <returns>Die ID des hinzugefügten Hunts oder -1 bei Fehler.</returns>
        public async Task<int> AddAsync(Hunt hunt)
        {
            try
            {
                await _context.ScavengerHunts.AddAsync(hunt);
                await _context.SaveChangesAsync();
                return hunt.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// Aktualisiert einen bestehenden Hunt.
        /// </summary>
        /// <param name="hunt">Der zu aktualisierende Hunt.</param>
        /// <returns>True, wenn erfolgreich, sonst false.</returns>
        public async Task<bool> UpdateAsync(Hunt hunt)
        {
            try
            {
                var existingHunt = await _context.ScavengerHunts
                                         .Include(h => h.Assignments)
                                         .FirstOrDefaultAsync(h => h.Id == hunt.Id);

                if (existingHunt is null)
                    return false;

                _context.Entry(existingHunt).State = EntityState.Detached;

                // Update the main entity
                _context.Entry(hunt).State = EntityState.Modified;

                // Handle Assignments
                foreach (var assignment in existingHunt.Assignments.ToList())
                {
                    // Remove hunts to maintain order
                    _context.Assignments.Remove(assignment);
                }

                foreach (var assignment in hunt.Assignments)
                {
                    var existingAssignment = existingHunt.Assignments.FirstOrDefault(a => a.Id == assignment.Id);

                    if (existingAssignment is null)
                        _context.Assignments.Add(assignment);

                    else
                        _context.Entry(existingAssignment).CurrentValues.SetValues(assignment);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Löscht einen Hunt anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Hunts.</param>
        /// <returns>Der gelöschte Hunt oder null, wenn nicht gefunden.</returns>
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
