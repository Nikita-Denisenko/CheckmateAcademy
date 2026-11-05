namespace CheckmateAcademy.Application.DTOs.Responses.ChessGame
{
    public record MakeMoveRequest
    {
        public string Move { get; init; }

        public MakeMoveRequest(string move)
        {
            if (string.IsNullOrWhiteSpace(move))
                throw new ArgumentException("Move cannot be null or empty.", nameof(move));

            Move = move;
        }
    }
}
