using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        public Task CreateExerciseAsync(Exercise exercise, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteExerciseAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Exercise>> GetAllExercisesAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Exercise?> GetExerciseByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateExerciseAsync(Exercise exercise, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
