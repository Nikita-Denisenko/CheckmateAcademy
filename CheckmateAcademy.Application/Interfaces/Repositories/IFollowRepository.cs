using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IFollowRepository
    {
        public Task CreateFollowAsync(Follow follow, CancellationToken ct);
        public Task<Follow?> GetFollowByIdAsync(int id, CancellationToken ct);
        public Task<List<Follow>> GetAllFollowsAsync(CancellationToken ct);
        public Task UpdateFollowAsync(Follow follow, CancellationToken ct);
        public Task DeleteFollowAsync(int id, CancellationToken ct);
        public Task<int> GetNextIdAsync(CancellationToken ct);
    }
}
