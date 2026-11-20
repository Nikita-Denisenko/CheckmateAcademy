using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IUserTaskProgressRepository
    {
        public Task CreateUserTaskProgressAsync(UserTaskProgress userTaskProgress, CancellationToken ct);
        public Task<UserTaskProgress?> GetUserTaskProgressByIdAsync(int id, CancellationToken ct);
        public Task<List<UserTaskProgress>> GetAllUserTaskProgressesAsync(CancellationToken ct);
        public Task UpdateUserTaskProgressAsync(UserTaskProgress userTaskProgress, CancellationToken ct);
        public Task DeleteUserTaskProgressAsync(int id, CancellationToken ct);
    }
}
