using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class Knight : Piece
    {
        public override string Name => nameof(Knight);

        public Knight(Color color, BoardPosition position) : base(color, position)
        {
        }

        public override bool IsValidMove(BoardPosition newPosition)
        {
            if (newPosition == Position)
                return false;

            int rowDifference = Math.Abs(newPosition.Row - Position.Row);
            int columnDifference = Math.Abs(newPosition.Column - Position.Column);

            return (rowDifference == 2 && columnDifference == 1) 
                || (rowDifference == 1 && columnDifference == 2);
        }

        public override bool CanJumpOverPieces => true;
    }
}
