using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class UserExerciseProgressRepository : IUserExerciseProgressRepository
    {
        private readonly StorageOptions _storageOptions;
        private readonly IExerciseRepository _exerciseRepository;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public UserExerciseProgressRepository(
            StorageOptions storageOptions,
            IExerciseRepository exerciseRepository)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));

            _exerciseRepository = exerciseRepository
                ?? throw new ArgumentNullException(nameof(exerciseRepository));
        }

        public async Task CreateUserExerciseProgressAsync(
            UserExerciseProgress userExerciseProgress,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(userExerciseProgress);

            var progresses = await ReadAllDataAsync(ct);

            bool alreadyExists = progresses.Any(progress =>
                progress.UserId == userExerciseProgress.UserId &&
                progress.ExerciseId == userExerciseProgress.ExerciseId);

            if (alreadyExists)
            {
                throw new InvalidOperationException(
                    "User exercise progress already exists.");
            }

            progresses.Add(
                UserExerciseProgressMapper.ToData(userExerciseProgress));

            await WriteAllDataAsync(progresses, ct);
        }

        public async Task DeleteUserExerciseProgressAsync(
            int userId,
            int exerciseId,
            CancellationToken ct)
        {
            var progresses = await ReadAllDataAsync(ct);

            int removedCount = progresses.RemoveAll(progress =>
                progress.UserId == userId &&
                progress.ExerciseId == exerciseId);

            if (removedCount == 0)
                return;

            await WriteAllDataAsync(progresses, ct);
        }

        public async Task<List<UserExerciseProgress>>
            GetAllUserExerciseProgressesAsync(CancellationToken ct)
        {
            var data = await ReadAllDataAsync(ct);
            var progresses = new List<UserExerciseProgress>();

            foreach (var progressData in data)
            {
                var exercise = await _exerciseRepository
                    .GetExerciseByIdAsync(progressData.ExerciseId, ct);

                if (exercise == null)
                {
                    throw new InvalidOperationException(
                        $"Exercise with ID {progressData.ExerciseId} was not found.");
                }

                progresses.Add(
                    UserExerciseProgressMapper.ToDomain(
                        progressData,
                        exercise));
            }

            return progresses;
        }

        public async Task<UserExerciseProgress?>
            GetUserExerciseProgressAsync(
                int userId,
                int exerciseId,
                CancellationToken ct)
        {
            var progresses = await GetAllUserExerciseProgressesAsync(ct);

            return progresses.FirstOrDefault(progress =>
                progress.UserId == userId &&
                progress.ExerciseId == exerciseId);
        }

        public async Task UpdateUserExerciseProgressAsync(
            UserExerciseProgress userExerciseProgress,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(userExerciseProgress);

            var progresses = await ReadAllDataAsync(ct);

            int index = progresses.FindIndex(progress =>
                progress.UserId == userExerciseProgress.UserId &&
                progress.ExerciseId == userExerciseProgress.ExerciseId);

            if (index == -1)
                return;

            progresses[index] = UserExerciseProgressMapper.ToData(
                userExerciseProgress);

            await WriteAllDataAsync(progresses, ct);
        }

        private async Task<List<UserExerciseProgressData>> ReadAllDataAsync(
            CancellationToken ct)
        {
            EnsureFileExists();

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.UsersExercisesProgressFilePath,
                ct);

            var progresses = new List<UserExerciseProgressData>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var data = JsonSerializer.Deserialize<UserExerciseProgressData>(
                    line,
                    JsonOptions);

                if (data != null)
                    progresses.Add(data);
            }

            return progresses;
        }

        private async Task WriteAllDataAsync(
            List<UserExerciseProgressData> progresses,
            CancellationToken ct)
        {
            EnsureFileExists();

            var lines = progresses.Select(progress =>
                JsonSerializer.Serialize(progress, JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.UsersExercisesProgressFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.UsersExercisesProgressFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
            {
                using var stream = File.Create(filePath);
            }
        }
    }
}
