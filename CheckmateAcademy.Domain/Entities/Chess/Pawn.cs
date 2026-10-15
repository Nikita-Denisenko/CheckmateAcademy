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
            throw new NotImplementedException();
        }
    }
}
