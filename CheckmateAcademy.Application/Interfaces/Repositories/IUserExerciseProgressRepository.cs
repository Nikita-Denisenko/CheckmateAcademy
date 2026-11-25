using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IUserExerciseProgressRepository
    {
        public Task CreateUserExerciseProgressAsync(UserExerciseProgress userExerciseProgress, CancellationToken ct);
        public Task<UserExerciseProgress?> GetUserExerciseProgressAsync(int userId, int exerciseId, CancellationToken ct);
        public Task<List<UserExerciseProgress>> GetAllUserExerciseProgressesAsync(CancellationToken ct);
        public Task UpdateUserExerciseProgressAsync(UserExerciseProgress userExerciseProgress, CancellationToken ct);
        public Task DeleteUserExerciseProgressAsync(int userId, int exerciseId, CancellationToken ct);
    }
}
