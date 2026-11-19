using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class TaskRepository : IChessTaskRepository
    {
        public Task CreateChessTaskAsync(ChessTask chessTask, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChessTaskAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChessTask>> GetAllChessTasksAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<ChessTask?> GetChessTaskByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateChessTaskAsync(ChessTask chessTask, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
