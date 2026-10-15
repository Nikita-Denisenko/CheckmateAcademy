using CheckmateAcademy.Domain.Common;
using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities
{
    public class Game
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int BotId { get; private set; }

        public ChessBoard ChessBoard { get; private set; } = null!;

        private readonly List<Move> _moves = [];

        public IReadOnlyCollection<Move> Moves => _moves;

        public Game(
            int id,
            int userId,
            int botId)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.PositiveId(userId, nameof(userId));
            DomainValidation.PositiveId(botId, nameof(botId));

            Id = id;
            UserId = userId;
            BotId = botId;

            ChessBoard = new ChessBoard();
        }

        public void MakeMove(Move move)
        {
            DomainValidation.NotNull(move, nameof(move));

            ChessBoard.MakeMove(move);

            _moves.Add(move);
        }
    }
}
