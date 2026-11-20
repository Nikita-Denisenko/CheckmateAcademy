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
            PropertyNameCaseInsensitive = true
        };

        public GameRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateGameAsync(Game game, CancellationToken ct)
        {
            EnsureFileExists();

            var gameData = GameMapper.ToData(game);
            var json = JsonSerializer.Serialize(gameData, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.GamesFilePath,
                json + Environment.NewLine,
                ct);
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

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.GamesFilePath,
                ct);

            var games = new List<Game>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var gameData = JsonSerializer.Deserialize<GameData>(
                    line,
                    JsonOptions);

                if (gameData is null)
                    continue;

                games.Add(GameMapper.ToDomain(gameData));
            }

            return games;
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
            var lines = games.Select(game =>
                JsonSerializer.Serialize(
                    GameMapper.ToData(game),
                    JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.GamesFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.GamesFilePath;
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

