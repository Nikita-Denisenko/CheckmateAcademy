using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        public Task CreateGameAsync(Game game, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGameAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Game>> GetAllGamesAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Game?> GetGameByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGameAsync(Game game, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
