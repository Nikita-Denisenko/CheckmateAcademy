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

        public BotRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions;
        }

        public async Task CreateBotAsync(Bot bot, CancellationToken ct)
        {
            var botData = BotMapper.ToData(bot);

            var botDataString = JsonSerializer.Serialize(botData);
            await File.AppendAllTextAsync(_storageOptions.BotsFilePath, botDataString + Environment.NewLine, ct);
        }

        public async Task DeleteBotAsync(int id, CancellationToken ct)
        {
            var bots = await GetAllBotsAsync(ct);

            var bot = bots.FirstOrDefault(b => b.Id == id)
                ?? throw new InvalidOperationException("Bot not found");

            bots.Remove(bot);

            await File.WriteAllLinesAsync(_storageOptions.BotsFilePath, bots
                .Select(b => JsonSerializer.Serialize(BotMapper.ToData(b))), ct);
        }

        public async Task<List<Bot>> GetAllBotsAsync(CancellationToken ct)
        {
            if (!File.Exists(_storageOptions.BotsFilePath))
                return new List<Bot>();

            var botLines = await File.ReadAllLinesAsync(_storageOptions.BotsFilePath, ct);

            return botLines
                .Select(line => JsonSerializer.Deserialize<BotData>(line))
                .Where(botData => botData != null)
                .Select(botData => BotMapper.ToDomain(botData!))
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

            var botLines = bots.Select(b => JsonSerializer.Serialize(BotMapper.ToData(b)));

            await File.WriteAllLinesAsync(_storageOptions.BotsFilePath, botLines, ct);
        }
    }
}
