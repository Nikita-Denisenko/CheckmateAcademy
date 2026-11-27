using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public ExerciseRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateExerciseAsync(Exercise exercise, CancellationToken ct)
        {
            var exercises = await GetAllExercisesAsync(ct);

            exercises.Add(exercise);

            await WriteExercisesAsync(exercises, ct);
        }

        public async Task DeleteExerciseAsync(int id, CancellationToken ct)
        {
            var exercises = await GetAllExercisesAsync(ct);

            var exercise = exercises.FirstOrDefault(e => e.Id == id)
                ?? throw new InvalidOperationException("Exercise not found");

            exercises.Remove(exercise);

            await WriteExercisesAsync(exercises, ct);
        }

        public async Task<List<Exercise>> GetAllExercisesAsync(CancellationToken ct)
        {
            EnsureFileExists();

            var json = await File.ReadAllTextAsync(
                _storageOptions.ExercisesFilePath,
                ct);

            var exercisesData = JsonSerializer.Deserialize<List<ExerciseData>>(
                json,
                JsonOptions) ?? [];

            return exercisesData
                .Select(ExerciseMapper.ToDomain)
                .ToList();
        }

        public async Task<Exercise?> GetExerciseByIdAsync(int id, CancellationToken ct)
        {
            var exercises = await GetAllExercisesAsync(ct);

            return exercises.FirstOrDefault(e => e.Id == id);
        }

        public async Task UpdateExerciseAsync(Exercise exercise, CancellationToken ct)
        {
            var exercises = await GetAllExercisesAsync(ct);

            var index = exercises.FindIndex(e => e.Id == exercise.Id);

            if (index == -1)
                throw new InvalidOperationException("Exercise not found");

            exercises[index] = exercise;

            await WriteExercisesAsync(exercises, ct);
        }

        private async Task WriteExercisesAsync(
            IEnumerable<Exercise> exercises,
            CancellationToken ct)
        {
            var exercisesData = exercises
                .Select(ExerciseMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(exercisesData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.ExercisesFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.ExercisesFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllExercisesAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
