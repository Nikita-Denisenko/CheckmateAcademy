using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public FollowRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateFollowAsync(Follow follow, CancellationToken ct)
        {
            EnsureFileExists();

            var followData = FollowMapper.ToData(follow);
            var json = JsonSerializer.Serialize(followData, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.FollowsFilePath,
                json + Environment.NewLine,
                ct);
        }

        public async Task DeleteFollowAsync(int id, CancellationToken ct)
        {
            var follows = await GetAllFollowsAsync(ct);

            var follow = follows.FirstOrDefault(f => f.Id == id)
                ?? throw new InvalidOperationException("Follow not found");

            follows.Remove(follow);

            await WriteFollowsAsync(follows, ct);
        }

        public async Task<List<Follow>> GetAllFollowsAsync(CancellationToken ct)
        {
            EnsureFileExists();

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.FollowsFilePath,
                ct);

            var follows = new List<Follow>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var followData = JsonSerializer.Deserialize<FollowData>(
                    line,
                    JsonOptions);

                if (followData is null)
                    continue;

                follows.Add(FollowMapper.ToDomain(followData));
            }

            return follows;
        }

        public async Task<Follow?> GetFollowByIdAsync(int id, CancellationToken ct)
        {
            var follows = await GetAllFollowsAsync(ct);

            return follows.FirstOrDefault(f => f.Id == id);
        }

        public async Task UpdateFollowAsync(Follow follow, CancellationToken ct)
        {
            var follows = await GetAllFollowsAsync(ct);

            var index = follows.FindIndex(f => f.Id == follow.Id);

            if (index == -1)
                throw new InvalidOperationException("Follow not found");

            follows[index] = follow;

            await WriteFollowsAsync(follows, ct);
        }

        private async Task WriteFollowsAsync(
            IEnumerable<Follow> follows,
            CancellationToken ct)
        {
            var lines = follows.Select(follow =>
                JsonSerializer.Serialize(
                    FollowMapper.ToData(follow),
                    JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.FollowsFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.FollowsFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
            {
                using var stream = File.Create(filePath);
            }
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllFollowsAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
