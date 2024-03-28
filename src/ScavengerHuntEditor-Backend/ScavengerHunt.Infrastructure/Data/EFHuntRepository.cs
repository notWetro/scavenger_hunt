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
    public sealed class EFHuntRepository : IHuntRepository
    {
        private ScavHuntDbContext _context;

        public EFHuntRepository(ScavHuntDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Hunt?> GetByIdAsync(int id)
        {
            return await _context.ScavengerHunts.FirstOrDefaultAsync(sh => sh.Id == id);
        }

        public async Task Add(Hunt hunt)
        {
            await _context.ScavengerHunts.AddAsync(hunt);
            _context.SaveChanges();
        }
    }
}
