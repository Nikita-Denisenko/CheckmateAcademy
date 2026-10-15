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
            throw new NotImplementedException();
        }

        public override bool CanJumpOverPieces => true;
    }
}
