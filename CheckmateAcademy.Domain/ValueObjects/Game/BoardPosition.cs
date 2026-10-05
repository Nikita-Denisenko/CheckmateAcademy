using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.ValueObjects.Game
{
    public readonly record struct BoardPosition
    {
        public int Row { get; }
        public int Column { get; }

        public BoardPosition(int row, int column)
        {
            ChessValidator.ValidatePosition(row, column);

            Row = row;
            Column = column;
        }
    }
}
