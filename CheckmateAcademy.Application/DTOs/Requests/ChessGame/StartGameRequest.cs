namespace CheckmateAcademy.Application.DTOs.Requests.ChessGame
{
    public record StartGameRequest
    {
        public int UserId { get; init; }
        public int BotId { get; init; }

        public StartGameRequest(int userId, int botId)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than zero.", nameof(userId));
            if (botId <= 0)
                throw new ArgumentException("BotId must be greater than zero.", nameof(botId));

            UserId = userId;
            BotId = botId;
        }
    }
}
