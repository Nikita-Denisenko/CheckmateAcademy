using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IChessTaskRepository
    {
        public Task CreateChessTaskAsync(ChessTask chessTask, CancellationToken ct);
        public Task<ChessTask?> GetChessTaskByIdAsync(int id, CancellationToken ct);
        public Task<List<ChessTask>> GetAllChessTasksAsync(CancellationToken ct);
        public Task UpdateChessTaskAsync(ChessTask chessTask, CancellationToken ct);
        public Task DeleteChessTaskAsync(int id, CancellationToken ct);
        public Task<int> GetNextIdAsync(CancellationToken ct);
    }
}
