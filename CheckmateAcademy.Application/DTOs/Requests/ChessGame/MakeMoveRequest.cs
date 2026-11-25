namespace CheckmateAcademy.Application.DTOs.Requests.ChessGame
{
    public record MakeMoveRequest
    {
        public string Move { get; init; }
        public int GameId { get; init; }
        public int UserId { get; init; }

        public MakeMoveRequest(string move, int gameId, int userId)
        {
            if (string.IsNullOrWhiteSpace(move))
                throw new ArgumentException("Move cannot be null or empty.", nameof(move));

            Move = move;
            GameId = gameId;
            UserId = userId;
        }
    }
}
