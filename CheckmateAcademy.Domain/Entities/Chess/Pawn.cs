using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class Pawn : Piece
    {
        public Pawn(Color color, BoardPosition position) : base(color, position) { }

        public override string Name => nameof(Pawn);

        public override bool IsValidMove(BoardPosition newPosition)
        {
            if (newPosition == Position)
                return false;

            int direction = Color == Color.White ? -1 : 1;

            int rowDifference = newPosition.Row - Position.Row;
            int columnDifference = newPosition.Column - Position.Column;

            if (columnDifference == 0 && rowDifference == direction)
                return true;

            if (columnDifference == 0 &&
                rowDifference == 2 * direction &&
                IsOnStartingPosition())
            {
                return true;
            }

            return false;
        }

        private bool IsOnStartingPosition()
        {
            return Color == Color.White
                ? Position.Row == 7
                : Position.Row == 2;
        }
    }
}
