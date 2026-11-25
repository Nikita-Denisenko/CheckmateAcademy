using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IUserTaskProgressRepository
    {
        public Task CreateUserTaskProgressAsync(UserTaskProgress userTaskProgress, CancellationToken ct);
        public Task<UserTaskProgress> GetUserTaskProgressAsync(int userId, int taskId, CancellationToken ct);
        public Task<List<UserTaskProgress>> GetAllUserTaskProgressesAsync(CancellationToken ct);
        public Task UpdateUserTaskProgressAsync(UserTaskProgress userTaskProgress, CancellationToken ct);
        public Task DeleteUserTaskProgressAsync(int userId, int taskId, CancellationToken ct);
    }
}
