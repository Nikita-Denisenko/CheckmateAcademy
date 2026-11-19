using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IGameRepository
    {
        public Task CreateGameAsync(Game game, CancellationToken ct);
        public Task<Game?> GetGameByIdAsync(int id, CancellationToken ct);
        public Task<List<Game>> GetAllGamesAsync(CancellationToken ct);
        public Task UpdateGameAsync(Game game, CancellationToken ct);
        public Task DeleteGameAsync(int id, CancellationToken ct);
    }
}
