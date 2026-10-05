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

        protected Piece(
            Color color,
            BoardPosition position)
        {
            Color = color;
            Position = position;
        }

        public void Move(BoardPosition newPosition)
        {
            if (!IsValidMove(newPosition))
                throw new DomainException($"Invalid move for {Name} from {Position} to {newPosition}");

            Position = newPosition;
        }

        public abstract bool IsValidMove(BoardPosition newPosition);
    }
}
