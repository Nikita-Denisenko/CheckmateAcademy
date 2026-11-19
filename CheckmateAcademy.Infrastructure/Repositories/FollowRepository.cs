using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        public Task CreateFollowAsync(Follow follow, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFollowAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Follow>> GetAllFollowsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Follow?> GetFollowByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFollowAsync(Follow follow, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
