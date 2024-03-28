using ScavengerHunt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Repositories
{
    public interface IStationRepository
    {
        public Task<Station> GetByIdAsync(int id);
        public Task Add(Station station);
    }
}
