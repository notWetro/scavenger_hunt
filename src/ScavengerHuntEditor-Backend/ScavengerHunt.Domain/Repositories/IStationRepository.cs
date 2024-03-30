using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Domain.Repositories
{
    public interface IStationRepository
    {
        public Task<Station?> GetByIdAsync(int id);
        public Task<IEnumerable<Station>> GetAll();

        public Task<IEnumerable<Station>> GetAllByHuntId(int huntId);

        public Task<int> AddAsync(Station station);

        public Task<bool> UpdateAsync(Station station);
        public Task<Station?> DeleteByIdAsync(int id);
    }
}
