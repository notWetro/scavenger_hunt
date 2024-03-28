using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Domain.Repositories
{
    public interface IHuntRepository
    {
        public Task<Hunt?> GetByIdAsync(int id);
        public Task<IEnumerable<Hunt>> GetAll();

        public Task<int> AddAsync(Hunt hunt);
        public Task<int[]> AddRangeAsync(IEnumerable<Hunt> hunts);

        public Task<bool> UpdateAsync(Hunt hunt);
        public Task<Hunt?> DeleteByIdAsync(int id);
    }
}
