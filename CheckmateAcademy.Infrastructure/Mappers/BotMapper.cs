using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class BotMapper
    {
        public static BotData ToData(Bot bot)
        {
            return new BotData
            {
                Id = bot.Id,
                Name = bot.Name,
                Rating = bot.Rating
            };
        }

        public static Bot ToDomain(BotData botData)
        {
            return new Bot(
                botData.Id,
                botData.Name,
                botData.Rating);
        }
    }
}
