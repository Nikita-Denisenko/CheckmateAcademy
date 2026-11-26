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
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public BotRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateBotAsync(Bot bot, CancellationToken ct)
        {
            EnsureFileExists();

            var botData = BotMapper.ToData(bot);
            var json = JsonSerializer.Serialize(botData, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.BotsFilePath,
                json + Environment.NewLine,
                ct);
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

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.BotsFilePath,
                ct);

            var bots = new List<Bot>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                var botData = JsonSerializer.Deserialize<BotData>(
                    line,
                    JsonOptions);

                if (botData is null)
                    continue;

                bots.Add(BotMapper.ToDomain(botData));
            }

            return bots;
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
            var lines = bots.Select(bot =>
                JsonSerializer.Serialize(
                    BotMapper.ToData(bot),
                    JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.BotsFilePath,
                lines,
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
                using var stream = File.Create(filePath);
            }
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllBotsAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
