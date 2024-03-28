using ScavengerHunt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Repositories
{
    public interface IHuntRepository
    {
        public Task<Hunt?> GetByIdAsync(int id);
        public Task Add(Hunt hunt);
    }
}
