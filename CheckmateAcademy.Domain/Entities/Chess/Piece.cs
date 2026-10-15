using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.Exceptions;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public abstract class Piece
    {
        public Color Color { get; }
        public BoardPosition Position { get; private set; }
        public abstract string Name { get; }

        public bool HasMoved { get; private set; }

        public Piece(Color color, BoardPosition position)
        {
            Color = color;
            Position = position;
        }

        public void MoveTo(BoardPosition newPosition)
        {
            Position = newPosition;
            HasMoved = true;
        }

        internal void SetPositionForSimulation(BoardPosition position)
        {
            Position = position;
        }

        public abstract bool IsValidMove(BoardPosition newPosition);

        public virtual bool CanJumpOverPieces => false;
    }
}
