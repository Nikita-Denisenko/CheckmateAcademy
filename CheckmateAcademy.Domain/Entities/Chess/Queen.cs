using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class Queen : Piece
    {
        public override string Name => nameof(Queen);

        public Queen(Color color, BoardPosition position) : base(color, position) { }

        public override bool IsValidMove(BoardPosition newPosition)
        {
            if (newPosition == Position)
                return false;

            int rowDifference = Math.Abs(newPosition.Row - Position.Row);
            int columnDifference = Math.Abs(newPosition.Column - Position.Column);

            return rowDifference == 0
                || columnDifference == 0
                || rowDifference == columnDifference;
        }
    }
}
