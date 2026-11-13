using CheckmateAcademy.Domain.Common;
using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Exceptions;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities
{
    public class Game
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int BotId { get; private set; }

        public ChessBoardSetup InitialPosition { get; private set; }

        public ChessBoard ChessBoard { get; private set; }

        private readonly List<Move> _moves = [];

        public IReadOnlyCollection<Move> Moves => _moves.AsReadOnly();

        public Game(int id, int userId, int botId)
            : this(id, userId, botId, CreateDefaultSetup())
        {
        }

        public Game(
            int id,
            int userId,
            int botId,
            ChessBoardSetup initialPosition)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.PositiveId(userId, nameof(userId));
            DomainValidation.PositiveId(botId, nameof(botId));
            DomainValidation.NotNull(initialPosition, nameof(initialPosition));

            Id = id;
            UserId = userId;
            BotId = botId;

            InitialPosition = initialPosition;
            ChessBoard = new ChessBoard(initialPosition);
        }

        public void MakeMove(Move move)
        {
            DomainValidation.NotNull(move, nameof(move));

            ChessBoard.MakeMove(move);
            _moves.Add(move);
        }

        private static ChessBoardSetup CreateDefaultSetup()
        {
            var pieces = StartPositionInitializer.InitializePieces()
                .Select(piece => new PieceSetup(
                    GetPieceType(piece),
                    piece.Color,
                    piece.Position));

            return new ChessBoardSetup(pieces);
        }

        private static PieceType GetPieceType(Piece piece)
        {
            return piece switch
            {
                Pawn => PieceType.Pawn,
                Rook => PieceType.Rook,
                Knight => PieceType.Knight,
                Bishop => PieceType.Bishop,
                Queen => PieceType.Queen,
                King => PieceType.King,
                _ => throw new DomainException(
                    $"Unsupported piece type: {piece.GetType().Name}.")
            };
        }
    }
}
