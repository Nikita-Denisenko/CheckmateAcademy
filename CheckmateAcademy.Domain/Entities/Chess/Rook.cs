using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class Rook : Piece
    {
        public override string Name => nameof(Rook);

        public Rook(Color color, BoardPosition position) : base(color, position)
        {
        }

        public override bool IsValidMove(BoardPosition newPosition)
        {
            if (newPosition == Position)
                return false;

            if (newPosition.Row != Position.Row && newPosition.Column != Position.Column)
                return false;

            return true;
        }
    }
}
