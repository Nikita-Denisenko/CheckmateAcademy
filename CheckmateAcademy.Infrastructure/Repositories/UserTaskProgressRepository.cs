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
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public UserTaskProgressRepository(
            StorageOptions storageOptions,
            IChessTaskRepository taskRepository)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));

            _taskRepository = taskRepository
                ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public async Task CreateUserTaskProgressAsync(
            UserTaskProgress userTaskProgress,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(userTaskProgress);

            var progresses = await ReadAllDataAsync(ct);

            bool alreadyExists = progresses.Any(progress =>
                progress.UserId == userTaskProgress.UserId &&
                progress.TaskId == userTaskProgress.TaskId);

            if (alreadyExists)
            {
                throw new InvalidOperationException(
                    "User task progress already exists.");
            }

            progresses.Add(UserTaskProgressMapper.ToData(userTaskProgress));

            await WriteAllDataAsync(progresses, ct);
        }

        public async Task DeleteUserTaskProgressAsync(
            int userId,
            int taskId,
            CancellationToken ct)
        {
            var progresses = await ReadAllDataAsync(ct);

            int removedCount = progresses.RemoveAll(progress =>
                progress.UserId == userId &&
                progress.TaskId == taskId);

            if (removedCount == 0)
                return;

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

        public async Task<UserTaskProgress>
            GetUserTaskProgressAsync(
                int userId,
                int taskId,
                CancellationToken ct)
        {
            var progresses = await GetAllUserTaskProgressesAsync(ct);

            return progresses.FirstOrDefault(progress =>
                progress.UserId == userId &&
                progress.TaskId == taskId)!;
        }

        public async Task UpdateUserTaskProgressAsync(
            UserTaskProgress userTaskProgress,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(userTaskProgress);

            var progresses = await ReadAllDataAsync(ct);

            int index = progresses.FindIndex(progress =>
                progress.UserId == userTaskProgress.UserId &&
                progress.TaskId == userTaskProgress.TaskId);

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

            var json = await File.ReadAllTextAsync(
                _storageOptions.UserTasksProgressFilePath,
                ct);

            return JsonSerializer.Deserialize<List<UserTaskProgressData>>(
                json,
                JsonOptions) ?? [];
        }

        private async Task WriteAllDataAsync(
            List<UserTaskProgressData> progresses,
            CancellationToken ct)
        {
            var json = JsonSerializer.Serialize(progresses, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.UserTasksProgressFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.UserTasksProgressFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }
    }
}
