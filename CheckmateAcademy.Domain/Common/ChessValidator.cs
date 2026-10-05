using CheckmateAcademy.Domain.Exceptions;

namespace CheckmateAcademy.Domain.Common
{
    public static class ChessValidator
    {
        private const int BoardSize = 8;

        public static void ValidatePosition(int row, int column)
        {
            if (row < 1 || row > BoardSize || column < 1 || column > BoardSize)
                throw new DomainException("Invalid board position.");
        }
    }
}