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

        public Task<TaskBase> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task ITaskRepository.Add(TaskBase taskBase)
        {
            throw new NotImplementedException();
        }
    }
}
