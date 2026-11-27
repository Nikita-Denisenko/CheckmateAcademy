using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class BotRepository : IBotRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public BotRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateBotAsync(Bot bot, CancellationToken ct)
        {
            var bots = await GetAllBotsAsync(ct);

            bots.Add(bot);

            await WriteBotsAsync(bots, ct);
        }

        public async Task DeleteBotAsync(int id, CancellationToken ct)
        {
            var bots = await GetAllBotsAsync(ct);

            var bot = bots.FirstOrDefault(b => b.Id == id)
                ?? throw new InvalidOperationException("Bot not found");

            bots.Remove(bot);

            await WriteBotsAsync(bots, ct);
        }

        public async Task<List<Bot>> GetAllBotsAsync(CancellationToken ct)
        {
            EnsureFileExists();

            var json = await File.ReadAllTextAsync(
                _storageOptions.BotsFilePath,
                ct);

            var botsData = JsonSerializer.Deserialize<List<BotData>>(
                json,
                JsonOptions) ?? [];

            return botsData
                .Select(BotMapper.ToDomain)
                .ToList();
        }

        public async Task<Bot?> GetBotByIdAsync(int id, CancellationToken ct)
        {
            var bots = await GetAllBotsAsync(ct);

            return bots.FirstOrDefault(b => b.Id == id);
        }

        public async Task UpdateBotAsync(Bot bot, CancellationToken ct)
        {
            var bots = await GetAllBotsAsync(ct);

            var index = bots.FindIndex(b => b.Id == bot.Id);

            if (index == -1)
                throw new InvalidOperationException("Bot not found");

            bots[index] = bot;

            await WriteBotsAsync(bots, ct);
        }

        private async Task WriteBotsAsync(
            IEnumerable<Bot> bots,
            CancellationToken ct)
        {
            var botsData = bots
                .Select(BotMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(botsData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.BotsFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.BotsFilePath;
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
            var entities = await GetAllBotsAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
