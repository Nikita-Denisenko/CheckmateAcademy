namespace CheckmateAcademy.Application.DTOs.Responses.ChessGame
{
    public record GiveUpRequest
    {
        public int GameId { get; init; }
        public int UserId { get; init; }

        public GiveUpRequest(int gameId, int userId)
        {
            if (gameId <= 0)
                throw new ArgumentException("GameId must be greater than zero.", nameof(gameId));

            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than zero.", nameof(userId));

            GameId = gameId;
            UserId = userId;
        }
    }
}
