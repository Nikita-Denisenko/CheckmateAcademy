using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class King : Piece
    {
        public King(Color color, BoardPosition position) : base(color, position)
        {
        }

        public override string Name => nameof(King);

        public override bool IsValidMove(BoardPosition newPosition)
        {
            if (newPosition == Position)
                return false;

            return Math.Abs(newPosition.Row - Position.Row) <= 1 
                && Math.Abs(newPosition.Column - Position.Column) <= 1;
        }
    }
}
