using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class Bishop : Piece
    {
        public Bishop(Color color, BoardPosition position) : base(color, position)
        {
        }

        public override string Name => nameof(Bishop);

        public override bool IsValidMove(BoardPosition newPosition)
        {
            if (newPosition == Position)
                return false;

            return Math.Abs(newPosition.Row - Position.Row) 
                == Math.Abs(newPosition.Column - Position.Column);      
        }
    }
}
