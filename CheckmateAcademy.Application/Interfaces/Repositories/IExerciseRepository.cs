using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IExerciseRepository
    {
        public Task CreateExerciseAsync(Exercise exercise, CancellationToken ct);
        public Task<Exercise?> GetExerciseByIdAsync(int id, CancellationToken ct);
        public Task<List<Exercise>> GetAllExercisesAsync(CancellationToken ct);
        public Task UpdateExerciseAsync(Exercise exercise, CancellationToken ct);
        public Task DeleteExerciseAsync(int id, CancellationToken ct);
    }
}
