using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IBotRepository
    {
        public Task CreateBotAsync(Bot bot, CancellationToken ct);
        public Task<Bot?> GetBotByIdAsync(int id, CancellationToken ct);
        public Task<List<Bot>> GetAllBotsAsync(CancellationToken ct);
        public Task UpdateBotAsync(Bot bot, CancellationToken ct);
        public Task DeleteBotAsync(int id, CancellationToken ct);
        public Task<int> GetNextIdAsync(CancellationToken ct);
    }
}
