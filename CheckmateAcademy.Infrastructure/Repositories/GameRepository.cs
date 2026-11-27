using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public GameRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateGameAsync(Game game, CancellationToken ct)
        {
            var games = await GetAllGamesAsync(ct);
            games.Add(game);
            await WriteGamesAsync(games, ct);
        }

        public async Task DeleteGameAsync(int id, CancellationToken ct)
        {
            var games = await GetAllGamesAsync(ct);

            var game = games.FirstOrDefault(g => g.Id == id)
                ?? throw new InvalidOperationException("Game not found");

            games.Remove(game);

            await WriteGamesAsync(games, ct);
        }

        public async Task<List<Game>> GetAllGamesAsync(CancellationToken ct)
        {
            EnsureFileExists();

            var json = await File.ReadAllTextAsync(
                _storageOptions.GamesFilePath,
                ct);

            var gamesData = JsonSerializer.Deserialize<List<GameData>>(
                json,
                JsonOptions) ?? [];

            return gamesData
                .Select(GameMapper.ToDomain)
                .ToList();
        }

        public async Task<Game?> GetGameByIdAsync(int id, CancellationToken ct)
        {
            var games = await GetAllGamesAsync(ct);

            return games.FirstOrDefault(g => g.Id == id);
        }

        public async Task UpdateGameAsync(Game game, CancellationToken ct)
        {
            var games = await GetAllGamesAsync(ct);

            var index = games.FindIndex(g => g.Id == game.Id);

            if (index == -1)
                throw new InvalidOperationException("Game not found");

            games[index] = game;

            await WriteGamesAsync(games, ct);
        }

        private async Task WriteGamesAsync(
            IEnumerable<Game> games,
            CancellationToken ct)
        {
            var gamesData = games
                .Select(GameMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(gamesData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.GamesFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.GamesFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllGamesAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
