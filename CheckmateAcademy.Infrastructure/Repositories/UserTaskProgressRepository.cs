
using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class UserTaskProgressRepository : IUserTaskProgressRepository
    {
        private readonly StorageOptions _storageOptions;
        private readonly IChessTaskRepository _taskRepository;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public UserTaskProgressRepository(
            StorageOptions storageOptions,
            IChessTaskRepository taskRepository)
        {
            _storageOptions = storageOptions;
            _taskRepository = taskRepository;
        }

        public async Task CreateUserTaskProgressAsync(
            UserTaskProgress userTaskProgress,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(userTaskProgress);

            EnsureFileExists();

            var data = UserTaskProgressMapper.ToData(userTaskProgress);

            string json = JsonSerializer.Serialize(data, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.UserTasksProgressFilePath,
                json + Environment.NewLine,
                ct);
        }

        public async Task DeleteUserTaskProgressAsync(
            int id,
            CancellationToken ct)
        {
            var progresses = await ReadAllDataAsync(ct);

            progresses.RemoveAll(progress => progress.Id == id);

            await WriteAllDataAsync(progresses, ct);
        }

        public async Task<List<UserTaskProgress>>
            GetAllUserTaskProgressesAsync(CancellationToken ct)
        {
            var data = await ReadAllDataAsync(ct);
            var progresses = new List<UserTaskProgress>();

            foreach (var progressData in data)
            {
                var task = await _taskRepository.GetChessTaskByIdAsync(
                    progressData.TaskId,
                    ct);

                if (task == null)
                {
                    throw new InvalidOperationException(
                        $"Task with ID {progressData.TaskId} was not found.");
                }

                progresses.Add(
                    UserTaskProgressMapper.ToDomain(
                        progressData,
                        task));
            }

            return progresses;
        }

        public async Task<UserTaskProgress?>
            GetUserTaskProgressByIdAsync(
                int id,
                CancellationToken ct)
        {
            var progresses = await GetAllUserTaskProgressesAsync(ct);

            return progresses.FirstOrDefault(
                progress => progress.Id == id);
        }

        public async Task UpdateUserTaskProgressAsync(
            UserTaskProgress userTaskProgress,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(userTaskProgress);

            var progresses = await ReadAllDataAsync(ct);

            int index = progresses.FindIndex(
                progress => progress.Id == userTaskProgress.Id);

            if (index == -1)
                return;

            progresses[index] = UserTaskProgressMapper.ToData(
                userTaskProgress);

            await WriteAllDataAsync(progresses, ct);
        }

        private async Task<List<UserTaskProgressData>> ReadAllDataAsync(
            CancellationToken ct)
        {
            EnsureFileExists();

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.UserTasksProgressFilePath,
                ct);

            var progresses = new List<UserTaskProgressData>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var data = JsonSerializer.Deserialize<UserTaskProgressData>(
                    line,
                    JsonOptions);

                if (data != null)
                    progresses.Add(data);
            }

            return progresses;
        }

        private async Task WriteAllDataAsync(
            List<UserTaskProgressData> progresses,
            CancellationToken ct)
        {
            EnsureFileExists();

            var lines = progresses.Select(progress =>
                JsonSerializer.Serialize(progress, JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.UserTasksProgressFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.UserTasksProgressFilePath;
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
