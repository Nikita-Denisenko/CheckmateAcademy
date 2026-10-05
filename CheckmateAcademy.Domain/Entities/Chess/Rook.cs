using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class Rook : Piece
    {
        public static IReadOnlyList<BoardPosition> ValidStartPositions { get; } =
        [
            new(1, 1),
            new(1, 8),
            new(8, 1),
            new(8, 8)
        ];

        public override string Name => nameof(Rook);

        public Rook(Color color, BoardPosition position) : base(color, position)
        {
        }

        public override bool IsValidMove(BoardPosition newPosition)
        {
            throw new NotImplementedException();
        }
    }
}
