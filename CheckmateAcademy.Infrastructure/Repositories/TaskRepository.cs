using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class TaskRepository : IChessTaskRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public TaskRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateChessTaskAsync(
            ChessTask chessTask,
            CancellationToken ct)
        {
            var chessTasks = await GetAllChessTasksAsync(ct);
            chessTasks.Add(chessTask);
            await WriteChessTasksAsync(chessTasks, ct);
        }

        public async Task DeleteChessTaskAsync(int id, CancellationToken ct)
        {
            var chessTasks = await GetAllChessTasksAsync(ct);

            var removedCount = chessTasks.RemoveAll(t => t.Id == id);

            if (removedCount == 0)
                return;

            await WriteChessTasksAsync(chessTasks, ct);
        }

        public async Task<List<ChessTask>> GetAllChessTasksAsync(
            CancellationToken ct)
        {
            EnsureFileExists();

            var json = await File.ReadAllTextAsync(
                _storageOptions.TasksFilePath,
                ct);

            var chessTasksData = JsonSerializer.Deserialize<List<ChessTaskData>>(
                json,
                JsonOptions) ?? [];

            return chessTasksData
                .Select(ChessTaskMapper.ToDomain)
                .ToList();
        }

        public async Task<ChessTask?> GetChessTaskByIdAsync(
            int id,
            CancellationToken ct)
        {
            var chessTasks = await GetAllChessTasksAsync(ct);

            return chessTasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task UpdateChessTaskAsync(
            ChessTask chessTask,
            CancellationToken ct)
        {
            var chessTasks = await GetAllChessTasksAsync(ct);

            var index = chessTasks.FindIndex(t => t.Id == chessTask.Id);

            if (index == -1)
                return;

            chessTasks[index] = chessTask;

            await WriteChessTasksAsync(chessTasks, ct);
        }

        private async Task WriteChessTasksAsync(
            IEnumerable<ChessTask> chessTasks,
            CancellationToken ct)
        {
            var chessTasksData = chessTasks
                .Select(ChessTaskMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(chessTasksData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.TasksFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.TasksFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllChessTasksAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
