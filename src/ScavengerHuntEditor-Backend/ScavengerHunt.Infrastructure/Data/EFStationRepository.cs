using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Repositories
{
    public sealed class EFStationRepository : IStationRepository
    {
        private ScavHuntDbContext _context;

        public EFStationRepository(ScavHuntDbContext dbContext)
        {
            _context = dbContext;
        }

        public Task Add(Station station)
        {
            throw new NotImplementedException();
        }

        public Task<Station> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
