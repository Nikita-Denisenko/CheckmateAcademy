using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.Exceptions;

namespace CheckmateAcademy.Domain.ValueObjects.Game
{
    public record Move
    {
        public Piece Piece { get; init; }
        public BoardPosition From { get; init; }
        public BoardPosition To { get; init; }  
        public MoveType Type { get; init; }

        public Move(
            Piece piece,
            BoardPosition from,
            BoardPosition to,
            MoveType type = MoveType.Normal)
        {
            if (from == to)
                throw new DomainException("From and To positions cannot be the same.");

            Piece = piece;
            Type = type;
            From = from;
            To = to;
        }
    }
}
