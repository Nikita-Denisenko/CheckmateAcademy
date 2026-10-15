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
            throw new NotImplementedException();
        }
    }
}
